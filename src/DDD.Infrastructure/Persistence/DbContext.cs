using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence;

public class DDDDbContext : DbContext
{
    public DDDDbContext(DbContextOptions<DDDDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(150);

            entity.Property(u => u.PassWordHash)
               .IsRequired()
               .HasMaxLength(200);

            entity.HasIndex(u => u.UserName).IsUnique();
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Route)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(x => x.Icon)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(x => x.Order)
                  .IsRequired();

            entity.Property(x => x.IsActive)
                  .IsRequired();
        });

        MenuSeed.Seed(modelBuilder);
    }
}
