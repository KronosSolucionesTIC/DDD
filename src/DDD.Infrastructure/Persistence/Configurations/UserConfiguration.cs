using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(u => u.PassWordHash)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasIndex(u => u.UserName).IsUnique();
    }
}
