using Microsoft.EntityFrameworkCore;
using NetCoreModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreRepository
{
    public class NetCoreDBContext : DbContext
    {
        public NetCoreDBContext(DbContextOptions<NetCoreDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<TimeRecord> TimeRecord { get; set; }

    }
}
