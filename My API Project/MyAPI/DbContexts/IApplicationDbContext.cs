using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Devices> Device { get; set; }

        DbSet<Zones> Zone { get; set; }

        DbSet<Categories> Category { get; set; }

        Task<int> SaveChanges();
    }
}
