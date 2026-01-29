using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DDD.Infrastructure.Persistence;

public class DDDDbContextFactory : IDesignTimeDbContextFactory<DDDDbContext>
{
    public DDDDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no encontrada.");

        var optionsBuilder = new DbContextOptionsBuilder<DDDDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new DDDDbContext(optionsBuilder.Options);
    }
}
