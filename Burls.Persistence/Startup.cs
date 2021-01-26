using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<BurlsDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlite(Configuration.GetConnectionString("BurlsDbContext"), sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("Burls.Persistence");
                });
            });
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
