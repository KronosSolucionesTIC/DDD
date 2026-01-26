using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.RegistrationDate)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasMany(c => c.Orders)
           .WithOne()                 
           .HasForeignKey("ClientId")
           .OnDelete(DeleteBehavior.Cascade);
    }
}
