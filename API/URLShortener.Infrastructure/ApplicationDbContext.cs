using Microsoft.EntityFrameworkCore;
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
    }
}
