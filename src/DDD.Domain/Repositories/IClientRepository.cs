using DDD.Domain.Entities;

namespace DDD.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task AddAsync(Client client);
        Task<Client?> GetByIdAsync(Guid id);
        Task UpdateAsync(Client client);
        Task<bool> ExistsAsync(Guid clientId);
    }
}
