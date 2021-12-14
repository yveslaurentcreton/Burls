using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Services;
using Burls.Application.Core.State;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // State
            services.AddSingleton<IApplicationState, ApplicationState>();
            services.AddSingleton<IBrowserState, BrowserState>();

            // Services
            services.AddScoped<IApplicationLifetimeService, ApplicationLifetimeService>();
            services.AddScoped<IBrowserService, BrowserService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IPathService, PathService>();
        }
    }

    public static class StartupExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var startup = new Startup(configuration);

            startup.ConfigureServices(services);
        }
    }
}
