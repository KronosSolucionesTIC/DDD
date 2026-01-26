using DDD.Application.Common;
using DDD.Application.Orders.DTOs;

namespace DDD.Application.Orders.Interfaces;

public interface IOrderReadRepository
{
    Task<PagedResult<OrderResponseDto>> GetPagedAsync(
        int page,
        int pageSize,
        Guid? clientId,
        DateTime? fromDate,
        DateTime? toDate
    );
}
