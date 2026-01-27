using DDD.Application.Clients.Commands;
using DDD.Application.Clients.Commands.CreateClient;
using DDD.Application.Clients.Commands.DeactivateClient;
using DDD.Application.Clients.DTOs;
using DDD.Application.Clients.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly GetAllClientsQuery _query;
        private readonly GetClient _getClient;
        private readonly CreateClientCommandHandler _createHandler;
        private readonly UpdateClientCommandHandler _updateHandler;
        private readonly DeactivateClientCommandHandler _deactivateHandler;

        public ClientsController(
            GetAllClientsQuery query,
            GetClient getClient,
            CreateClientCommandHandler createHandler,
            UpdateClientCommandHandler updateHandler,
            DeactivateClientCommandHandler deactivateHandler)
        {
            _query = query;
            _getClient = getClient;
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deactivateHandler = deactivateHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _query.ExecuteAsync();

            var response = ApiResponseMapper
                .FromResult(result, "Clientes obtenidos correctamente");

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var result = await _getClient.ExecuteAsync(id);

            var response = ApiResponseMapper
                .FromResult(result, "Clientes obtenidos correctamente");

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientCommand command)
        {
            var result = await _createHandler.Handle(command);

            var response = ApiResponseMapper
                .FromResult(result, "Cliente creado correctamente");

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(
                nameof(Create),
                new { id = result.Value },
                response
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateClientRequest request)
        {
            var command = new UpdateClientCommand(
                id,
                request.Name,
                request.Email
            );

            await _updateHandler.Handle(command);

            return Ok(ApiResponseMapper.Success("Cliente actualizado correctamente"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeactivateClientCommand(id);

            await _deactivateHandler.Handle(command);

            return Ok(ApiResponseMapper.Success("Cliente eliminado correctamente"));
        }
    }
}