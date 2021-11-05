using Burls.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence
{
    public class BurlsDbContext : DbContext
    {
        public DbSet<Browser> Browsers { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public BurlsDbContext(DbContextOptions<BurlsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BurlsDbContext).Assembly);
        }
    }
}
