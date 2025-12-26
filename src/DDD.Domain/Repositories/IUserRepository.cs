using DDD.Domain.Entities;

namespace DDD.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByUsernameAsync(string username);
    }
}
