using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DDD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO MenuItems (Id, Title, Route, Icon, `Order`, IsActive)
                VALUES
                ('11111111-1111-1111-1111-111111111111', 'Usuarios', '/users', 'users', 1, 1),
                ('22222222-2222-2222-2222-222222222222', 'Pagos', '/payments', 'payments', 2, 1);
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
