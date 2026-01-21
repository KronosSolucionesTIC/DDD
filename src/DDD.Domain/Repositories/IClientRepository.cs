using DDD.Domain.Entities;

namespace DDD.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
    }
}
