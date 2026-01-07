using DDD.Api.Middlewares;
using DDD.Application.Users.Commands;
using DDD.Application.Users.Queries;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DDDDbContext>(options =>
    options.UseMySql(
        "Server=localhost;Port=3306;Database=carolina_endara;User=root;Password=;",
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

builder.Services.AddControllers();
builder.Services.AddScoped<GetAllUsersQuery>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<LoginUserCommand>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<GetMenuQuery>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        p => p.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }