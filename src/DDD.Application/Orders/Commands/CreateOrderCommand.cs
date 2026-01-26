namespace DDD.Application.Orders.Commands
{
    public record CreateOrderCommand(
        Guid ClientId,
        decimal TotalAmount
    );
}
