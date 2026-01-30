using DDD.Application.Common;
using DDD.Application.Orders.DTOs;
using DDD.Application.Orders.Interfaces;
using DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly AppDbContext _context;

    public OrderReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<OrderResponseDto>> GetPagedAsync(
        int page,
        int pageSize,
        Guid? clientId,
        DateTime? fromDate,
        DateTime? toDate)
    {
        var query =
            from o in _context.Orders.AsNoTracking()
            join c in _context.Clients.AsNoTracking()
                on o.ClientId equals c.Id
            where o.IsActive
            select new { o, c };

        if (clientId.HasValue && clientId != Guid.Empty)
            query = query.Where(x => x.o.ClientId == clientId);

        if (fromDate.HasValue)
            query = query.Where(x => x.o.CreatedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.o.CreatedAt <= toDate.Value);

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.o.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new OrderResponseDto
            {
                Id = x.o.Id,
                IdCliente = x.c.Id,
                NombreCliente = x.c.Name,
                EmailCliente = x.c.Email,
                Total = x.o.TotalAmount
            })
            .ToListAsync();

        return new PagedResult<OrderResponseDto>(
            items,
            totalItems,
            page,
            pageSize
        );
    }

    public async Task<OrderResponseDto?> GetOrderAsync(Guid? Id)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.Client)
            .Where(o => o.Id == Id)
            .Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Total = o.TotalAmount,
                IdCliente = o.ClientId,
                NombreCliente = o.Client.Name,
                EmailCliente = o.Client.Email
            })
            .FirstOrDefaultAsync();
    }
}
