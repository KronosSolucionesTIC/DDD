using DDD.Application.Orders.DTOs;
using DDD.Application.Orders.Interfaces;
using DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly DDDDbContext _context;

    public OrderReadRepository(DDDDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        return await
        (
            from o in _context.Orders.Where(o => o.IsActive == true).AsNoTracking()
            join c in _context.Clients.AsNoTracking()
                on o.ClientId equals c.Id
            select new OrderResponseDto
            {
                OrderId = o.Id,
                ClientId = c.Id,
                ClientName = c.Name,
                ClientEmail = c.Email,
                TotalAmount = o.TotalAmount,
                CreatedAt = o.CreatedAt
            }
        ).ToListAsync();
    }
}
