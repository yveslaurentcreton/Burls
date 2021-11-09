using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Burls.Windows.Constants;
using Burls.Windows.Contracts.Services;
using Burls.Windows.Services;
using Burls.Windows.Models;
using Burls.Windows.ViewModels;
using Burls.Windows.Views;

using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using Burls.Core.Services;
using Burls.Persistence;
using System.ComponentModel;
using Unity;
using Burls.Windows.Helpers;
using AutoMapper;
using Prism.Regions;
using Prism.Modularity;
using System.Collections.ObjectModel;
using Burls.Application.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Burls.Application.Core.Commands;
using MahApps.Metro.Controls;
using Burls.Windows.Core;

namespace Burls.Windows
{
    // For more inforation about application lifecyle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview
    // For docs about using Prism in WPF see https://prismlibrary.com/docs/wpf/introduction.html

    // WPF UI elements use language en-US by default.
    // If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
    // Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
    public partial class App : PrismApplication
    {
        private string[] _startUpArgs;

        public App()
        {
        }
        
        protected override Window CreateShell()
        {
            // Init application
            var mediator = Container.Resolve<IMediator>();
            mediator.Send(new ApplicationInitializeCommand(_startUpArgs)).Wait();

            // Init theme
            var themeSelectorService = Container.Resolve<IThemeSelectorService>();
            themeSelectorService.SetTheme();

            // Create shell
            return MainWindow = InitializeShell() as ShellWindow;
        }

        private Window InitializeShell()
        {
            var regionManager = Container.Resolve<IRegionManager>();

            // Add flyouts
            regionManager.RegisterViewWithRegion(Regions.FlyoutRegion, typeof(ProfileSelectionRuleCreateDialog));

            // Create shell
            MainWindow = Container.Resolve<ShellWindow>();

            return MainWindow;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _startUpArgs = e.Args;

            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IServiceProvider, UnityProvider>();
            
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommandsProxy>();
            containerRegistry.RegisterSingleton<IFlyoutService, FlyoutService>();

            // Core Services
            containerRegistry.Register<IFileService, FileService>();

            // App Services
            containerRegistry.Register<IApplicationService, ApplicationService>();
            containerRegistry.Register<ISystemService, SystemService>();
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            containerRegistry.Register<IThemeSelectorService, ThemeSelectorService>();

            // Views
            containerRegistry.RegisterForNavigation<BrowserProfileSelectionPage, BrowserProfileSelectionViewModel>(PageKeys.BrowserProfileSelection);
            containerRegistry.RegisterForNavigation<BrowserProfileSetupPage, BrowserProfileSetupViewModel>(PageKeys.BrowserProfileSetup);
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>(PageKeys.Settings);
            containerRegistry.RegisterForNavigation<ShellWindow, ShellViewModel>();
            containerRegistry.RegisterForNavigation<ProfileSelectionRuleCreateDialog, ProfileSelectionRuleCreateViewModel>();

            // Configuration
            var configuration = BuildConfiguration();
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            containerRegistry.RegisterInstance(configuration);
            containerRegistry.RegisterInstance(appConfig);

            // Register services
            containerRegistry.RegisterServices(services =>
            {
                services.AddPresentationServices(configuration);
            });
        }

        private IConfiguration BuildConfiguration()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddJsonFile("appsettings.json")
                .AddCommandLine(_startUpArgs)
                .Build();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            var mediator = Container.Resolve<IMediator>();
            mediator.Send(new ApplicationFinalizeCommand()).Wait();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
        }
    }
}
