using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DDD.Infrastructure.Persistence;

public class UsersDbContextFactory
    : IDesignTimeDbContextFactory<DDDDbContext>
{
    public DDDDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DDDDbContext>();

        optionsBuilder.UseMySql(
            "Server=localhost;Port=3306;Database=carolina_endara;User=root;Password=;",
            new MySqlServerVersion(new Version(8, 0, 36))
        );

        return new DDDDbContext(optionsBuilder.Options);
    }
}
