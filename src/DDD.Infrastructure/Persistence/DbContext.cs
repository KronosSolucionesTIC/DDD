using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence;

public class DDDDbContext : DbContext
{
    public DDDDbContext(DbContextOptions<DDDDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<User> Users => Set<User>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DDDDbContext).Assembly);
    }
}
