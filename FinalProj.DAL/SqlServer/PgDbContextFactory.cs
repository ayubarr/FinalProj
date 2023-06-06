using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinalProj.DAL.SqlServer
{
    public class PgDbContextFactory : IDesignTimeDbContextFactory<PgDbContext>
    {
        public PgDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("PgDbContextConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PgDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new PgDbContext(optionsBuilder.Options);
        }
    }
}
