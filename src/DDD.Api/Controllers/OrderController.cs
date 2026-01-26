using DDD.Application.Orders.Commands;
using DDD.Application.Orders.Commands.DeactivateOrder;
using DDD.Application.Orders.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderCommandHandler _createHandler;
        private readonly GetAllOrdersEnrichedQuery _getAllOrdersEnrichedQuery;
        private readonly UpdateOrderCommandHandler _updateHandler;
        private readonly DeactivateOrderCommandHandler _deactivateHandler;

        public OrdersController(
            CreateOrderCommandHandler createOrderHandler, 
            GetAllOrdersEnrichedQuery getAllOrdersEnrichedQuery,
            UpdateOrderCommandHandler updateOrderHandler,
            DeactivateOrderCommandHandler deactivateOrderHandler)
        {
            _createHandler = createOrderHandler;
            _getAllOrdersEnrichedQuery = getAllOrdersEnrichedQuery;
            _updateHandler = updateOrderHandler;
            _deactivateHandler = deactivateOrderHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var result = await _createHandler.Handle(command);

            var response = ApiResponseMapper
                .FromResult(result, "Orden creada correctamente");

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(
                nameof(Create),
                new { id = result.Value },
                response
            );
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _getAllOrdersEnrichedQuery.ExecuteAsync();

            return Ok(ApiResponseMapper
                .FromResult(result, "Órdenes obtenidas correctamente"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateOrderRequest request)
        {
            var command = new UpdateOrderCommand
            {
                OrderId = id,
                ClientId = request.ClientId,
                TotalAmount = request.TotalAmount,
                Date = request.Date
            };

            var result = await _updateHandler.HandleAsync(command);

            return result.IsSuccess
                ? Ok(ApiResponseMapper.Success("Orden actualizada correctamente"))
                : BadRequest(ApiResponseMapper.Fail(result.Error));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeactivateOrderCommand(id);

            await _deactivateHandler.Handle(command);

            return Ok(ApiResponseMapper.Success("Orden eliminada correctamente"));
        }
    }
}
