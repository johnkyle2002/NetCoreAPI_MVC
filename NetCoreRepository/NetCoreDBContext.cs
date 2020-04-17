using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreRepository
{
    public class NetCoreDBContext : DbContext
    {
        public NetCoreDBContext(DbContextOptions<NetCoreDBContext> options):base(options)
        {

        }


    }
}
