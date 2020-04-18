using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetCoreRepository;
using System.IO;

namespace NetCoreMigrationBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NetCoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NetCoreDBContext"), b => b.MigrationsAssembly("NetCoreMigrationBuilder"));
        }
    }

    public class NetCoreContextContextFactory : IDesignTimeDbContextFactory<NetCoreDBContext>
    {
        public NetCoreDBContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NetCoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NetCoreDBContext"), b => b.MigrationsAssembly("NetCoreMigrationBuilder"));

            return new NetCoreDBContext(optionsBuilder.Options);
        }
    }
}
