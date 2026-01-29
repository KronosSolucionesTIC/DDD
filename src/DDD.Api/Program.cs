using DDD.Api.Middlewares;
using DDD.Application.Clients.Commands;
using DDD.Application.Clients.Commands.CreateClient;
using DDD.Application.Clients.Commands.DeactivateClient;
using DDD.Application.Clients.Queries;
using DDD.Application.Orders.Commands;
using DDD.Application.Orders.Commands.DeactivateOrder;
using DDD.Application.Orders.Interfaces;
using DDD.Application.Orders.Queries;
using DDD.Application.Users.Commands;
using DDD.Application.Users.Queries;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DDD.Application.Common.Security;
using DDD.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        ),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<DDDDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddControllers();

//Authentication
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

//Clients
builder.Services.AddScoped<CreateClientCommandHandler>();
builder.Services.AddScoped<UpdateClientCommandHandler>();
builder.Services.AddScoped<DeactivateClientCommandHandler>();
builder.Services.AddScoped<GetAllClientsQuery>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<GetClient>();

//Orders
builder.Services.AddScoped<CreateOrderCommandHandler>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<GetAllOrdersPaginationQuery>();
builder.Services.AddScoped<GetOrder>();
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

app.UseAuthentication();
app.UseAuthorization();

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