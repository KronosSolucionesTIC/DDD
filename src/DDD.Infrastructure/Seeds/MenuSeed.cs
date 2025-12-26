using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public static class MenuSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>().HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Usuarios",
                Route = "/users",
                Icon = "users",
                Order = 1,
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "Pagos",
                Route = "/payments",
                Icon = "payments",
                Order = 2,
                IsActive = true
            }
        );
    }
}
