using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
                  .IsRequired()
                  .HasMaxLength(100);

        builder.Property(x => x.Route)
                  .IsRequired()
                  .HasMaxLength(200);

        builder.Property(x => x.Icon)
                  .IsRequired()
                  .HasMaxLength(50);

        builder.Property(x => x.Order)
                  .IsRequired();

        builder.Property(x => x.IsActive)
                  .IsRequired();
    }
}
