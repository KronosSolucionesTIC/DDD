using DDD.Domain.Entities;
using DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetMenuAsync()
    {
        return await _context.MenuItems
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Order)
            .ToListAsync();
    }
}
