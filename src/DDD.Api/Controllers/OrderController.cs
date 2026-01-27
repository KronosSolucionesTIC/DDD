using DDD.Application.Common;
using DDD.Application.Orders.Commands;
using DDD.Application.Orders.Commands.DeactivateOrder;
using DDD.Application.Orders.DTOs;
using DDD.Application.Orders.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderCommandHandler _createHandler;
        private readonly GetAllOrdersPaginationQuery _getAllOrdersPaginationQuery;
        private readonly GetOrder _getOrder;
        private readonly UpdateOrderCommandHandler _updateHandler;
        private readonly DeactivateOrderCommandHandler _deactivateHandler;

        public OrdersController(
            CreateOrderCommandHandler createOrderHandler, 
            GetAllOrdersPaginationQuery getAllOrdersPaginationQuery,
            GetOrder getOrder,
            UpdateOrderCommandHandler updateOrderHandler,
            DeactivateOrderCommandHandler deactivateOrderHandler)
        {
            _createHandler = createOrderHandler;
            _getAllOrdersPaginationQuery = getAllOrdersPaginationQuery;
            _getOrder = getOrder;
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
        public async Task<IActionResult> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? clientId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var result = await _getAllOrdersPaginationQuery.ExecuteAsync(
                page,
                pageSize,
                clientId,
                fromDate,
                toDate
            );

            return Ok(
                ApiResponseMapper.FromResult(
                    result,
                    "Órdenes obtenidas correctamente"
                )
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _getOrder.ExecuteAsync(id);
            if (order == null)
            {
                return NotFound(
                    ApiResponseMapper.Fail("Orden no encontrada")
                );
            } 
            return Ok(
                ApiResponseMapper.FromResult(
                    Result<OrderResponseDto>.Success(order),
                    "Orden obtenida correctamente"
                )
            );
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
