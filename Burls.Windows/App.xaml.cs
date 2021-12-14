using Burls.Application.Browsers.Data;
using Burls.Application.Browsers.Services;
using Burls.Application.Core.Services;
using Burls.Persistence;
using Burls.Windows.Constants;
using Burls.Windows.Models;
using Burls.Windows.Pages;
using Burls.Windows.Services;
using Burls.Windows.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Burls.Windows
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Microsoft.UI.Xaml.Application
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _servicesCollection;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Create configuration
            _configuration = BuildConfiguration();

            // Register services
            _servicesCollection = new ServiceCollection();
            ConfigureServices(_servicesCollection);
            _serviceProvider = _servicesCollection.BuildServiceProvider();

            // Init application
            Task.Run(() => 
            {
                var applicationLifetimeService = _serviceProvider.GetService<IApplicationLifetimeService>();
                var startUpArgs = Environment.GetCommandLineArgs();
                applicationLifetimeService.Initialize(startUpArgs).Wait();
            });

            // Set application theme
            var applicationService = _serviceProvider.GetService<IApplicationService>();
            var theme = applicationService.GetTheme();

            if (Enum.TryParse<Microsoft.UI.Xaml.ApplicationTheme>(theme.ToString(), out var winuiTheme))
            {
                RequestedTheme = winuiTheme;
            }
        }

        private IConfiguration BuildConfiguration()
        {
            var appLocation = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddJsonFile("appsettings.json")
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            services.AddSingleton(_configuration);
            services.AddSingleton(_configuration.GetSection(nameof(AppSettings)).Get<AppSettings>());

            // Automapper
            services.AddAutoMapper(typeof(App));

            // Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(new NLogLoggingConfiguration(_configuration.GetSection("NLog")));
            });

            // Services
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddScoped<INavigationService, NavigationService>();
            services.AddScoped<IOperatingSystemService, WindowsService>();

            // Broadcasters
            services.AddSingleton<INavigationBroadcaster, NavigationBroadcaster>();

            // Managers
            services.AddSingleton<INavigationManager, NavigationManager>();

            // Stores
            services.AddScoped<INavigationStore, NavigationStore>();

            // Windows
            services.AddSingleton<MainWindow>();

            // Pages
            services.AddSingleton<ShellPage>();
            services.AddScoped<BrowserProfileSelectionPage>();

            // ViewModels
            services.AddScoped<BrowserProfileSelectionViewModel>();
            services.AddScoped<SettingsViewModel>();

            // Add application services
            services.AddApplicationServices(_configuration);

            // Add persistence services
            services.AddPersistenceServices(_configuration);

            // Add state notification services
            services.AddSingleton<IBrowserStateNotificationService, BrowserStateNotificationService>();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _serviceProvider.GetService<INavigationManager>().Subscribe();

            // Open window
            _serviceProvider.GetService<MainWindow>().Activate();

            // Set home page
            _serviceProvider.GetService<INavigationService>().Navigate(PageKeys.BrowserProfileSelection);
        }

    }
}
