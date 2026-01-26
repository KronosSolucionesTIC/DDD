using DDD.Application.Common;
using DDD.Application.Orders.DTOs;
using DDD.Application.Orders.Interfaces;

public class GetAllOrdersEnrichedQuery
{
    private readonly IOrderReadRepository _repository;

    public GetAllOrdersEnrichedQuery(IOrderReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<OrderResponseDto>>> ExecuteAsync()
    {
        var data = await _repository.GetAllAsync();

        return Result<IEnumerable<OrderResponseDto>>
            .Success(data);
    }
}
