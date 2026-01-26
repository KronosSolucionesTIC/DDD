using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence.Repositories;

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
                             .Where(x => x.IsActive)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _context.Clients.Where(x => x.IsActive).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid clientId)
    {
        return await _context.Clients
            .AsNoTracking()
            .AnyAsync(c => c.Id == clientId);
    }
}