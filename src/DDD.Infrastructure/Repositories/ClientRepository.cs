using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly DDDDbContext _context;

    public ClientRepository(DDDDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients
                             .AsNoTracking()
                             .ToListAsync();
    }
}