using DDD.Application.Common;
using DDD.Application.Orders.DTOs;
using DDD.Application.Orders.Interfaces;

public class GetAllOrdersPaginationQuery
{
    private readonly IOrderReadRepository _repository;

    public GetAllOrdersPaginationQuery(IOrderReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResult<OrderResponseDto>>> ExecuteAsync(
        int page,
        int pageSize,
        Guid? clientId,
        DateTime? fromDate,
        DateTime? toDate
    )
    {
        var data = await _repository.GetPagedAsync(
            page,
            pageSize,
            clientId,
            fromDate,
            toDate
        );

        return Result<PagedResult<OrderResponseDto>>.Success(data);
    }
}
