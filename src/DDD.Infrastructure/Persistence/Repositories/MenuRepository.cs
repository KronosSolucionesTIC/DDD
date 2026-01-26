using DDD.Domain.Entities;
using DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class MenuRepository : IMenuRepository
{
    private readonly DDDDbContext _context;

    public MenuRepository(DDDDbContext context)
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
