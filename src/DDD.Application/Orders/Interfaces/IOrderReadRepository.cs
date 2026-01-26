using DDD.Application.Orders.DTOs;

namespace DDD.Application.Orders.Interfaces;

public interface IOrderReadRepository
{
    Task<IEnumerable<OrderResponseDto>> GetAllAsync();
}
