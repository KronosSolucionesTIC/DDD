using DDD.Domain.Repositories;

namespace DDD.Application.Orders.Commands.DeactivateOrder;

public class DeactivateOrderCommandHandler
{
    private readonly IOrderRepository _repository;

    public DeactivateOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeactivateOrderCommand command)
    {
        var Order = await _repository.GetByIdAsync(command.OrderId);

        if (Order is null)
            throw new ApplicationException("Orden no encontrada");

        Order.Deactivate();

        await _repository.UpdateAsync(Order);
    }
}