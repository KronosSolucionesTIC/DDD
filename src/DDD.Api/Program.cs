using DDD.Api.Middlewares;
using DDD.Application.Clients.Commands;
using DDD.Application.Clients.Commands.CreateClient;
using DDD.Application.Clients.Commands.DeactivateClient;
using DDD.Application.Clients.Queries;
using DDD.Application.Orders.Commands;
using DDD.Application.Orders.Commands.DeactivateOrder;
using DDD.Application.Orders.Interfaces;
using DDD.Application.Users.Commands;
using DDD.Application.Users.Queries;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DDDDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

builder.Services.AddControllers();

//Clients
builder.Services.AddScoped<CreateClientCommandHandler>();
builder.Services.AddScoped<UpdateClientCommandHandler>();
builder.Services.AddScoped<DeactivateClientCommandHandler>();
builder.Services.AddScoped<GetAllClientsQuery>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

//Orders
builder.Services.AddScoped<CreateOrderCommandHandler>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<GetAllOrdersPaginationQuery>();
builder.Services.AddScoped<UpdateOrderCommandHandler>();
builder.Services.AddScoped<DeactivateOrderCommandHandler>();

//Users
builder.Services.AddScoped<GetAllUsersQuery>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Login
builder.Services.AddScoped<LoginUserCommand>();

//Menu
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<GetMenuQuery>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD.Api v1");
        c.RoutePrefix = "swagger"; // opcional
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }