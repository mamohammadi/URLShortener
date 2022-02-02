using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.Data.Entities;

namespace URLShortener.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {   
        public DbSet<URL> URLs { get; set; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public static ApplicationDbContext CreateInstance(string connectionString)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<URL>()
                .HasIndex(u => u.ShortURLVersion)
                .IsUnique();

            modelBuilder.Entity<URL>()
                .HasIndex(u => u.LongURLVersion)
                .IsUnique();
        }
    }
}
