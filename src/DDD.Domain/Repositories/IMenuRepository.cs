using DDD.Domain.Entities;

public interface IMenuRepository
{
    Task<IEnumerable<MenuItem>> GetMenuAsync();
}
