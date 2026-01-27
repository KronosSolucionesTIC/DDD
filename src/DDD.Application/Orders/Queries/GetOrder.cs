using DDD.Application.Orders.DTOs;
using DDD.Application.Orders.Interfaces;

namespace DDD.Application.Orders.Queries
{
    public class GetOrder
    {
        private readonly IOrderReadRepository _orderReadRepository;
        public GetOrder(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<OrderResponseDto?> ExecuteAsync(Guid orderId)
        {
            var order = await _orderReadRepository.GetOrderAsync(orderId);
            if (order == null)
                return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                IdCliente   = order.IdCliente,
                Total = order.Total,
                EmailCliente = order.EmailCliente,
                NombreCliente = order.NombreCliente
            };
        }
    }
}
