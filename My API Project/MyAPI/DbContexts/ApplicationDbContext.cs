using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<Devices> Device { get; set; }

        public DbSet<Zones> Zone { get; set; }

        public DbSet<Categories> Category { get; set; }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
