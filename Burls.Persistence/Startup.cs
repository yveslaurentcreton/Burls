using Burls.Application.Browsers.Data;
using Burls.Application.Browsers.Services;
using Burls.Application.Core.Data;
using Burls.Application.Core.Services;
using Burls.Domain;
using Burls.Persistence.Browsers.Data;
using Burls.Persistence.Core;
using Burls.Persistence.Core.Data;
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
            // Repositories
            services.AddSingleton<ISettingsRepository, SettingsRepository>();
            services.AddScoped<IBrowserRepository, BrowserRepository>();

            // Services
            services.AddScoped<ISettingsService, SettingsService>();
        }
    }

    public static class StartupExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var startup = new Startup(configuration);

            startup.ConfigureServices(services);
        }
    }
}
