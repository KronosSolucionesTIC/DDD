using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DDDDbContext _context;

    public UserRepository(DDDDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
                             .AsNoTracking()
                             .ToListAsync();
    }
}
