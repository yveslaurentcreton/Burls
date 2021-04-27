using Burls.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows
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
            // Automapper
            services.AddAutoMapper(typeof(App));

            // Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(new NLogLoggingConfiguration(Configuration.GetSection("NLog")));
            });

            // Add application
            services.AddApplicationServices(Configuration);

            // Add persistence
            services.AddPersistenceServices(Configuration);
        }
    }

    public static class StartupExtensions
    {
        public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var startup = new Startup(configuration);

            startup.ConfigureServices(services);
        }
    }
}
