using DDD.Application.Common;
using DDD.Application.Orders.Commands;
using DDD.Domain.Repositories;

public class UpdateOrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IClientRepository clientRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
    }

    public async Task<Result> HandleAsync(UpdateOrderCommand command)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId);

        if (order is null)
            return Result.Failure("La orden no existe");

        var clientExists = await _clientRepository.ExistsAsync(command.ClientId);
        if (!clientExists)
            return Result.Failure("El cliente no existe");

        order.UpdateClient(command.ClientId);
        order.UpdateTotalAmount(command.TotalAmount);
        order.UpdateDate(command.Date);

        await _orderRepository.UpdateAsync(order);

        return Result.Success("Orden modificada correctamente");
    }
}
