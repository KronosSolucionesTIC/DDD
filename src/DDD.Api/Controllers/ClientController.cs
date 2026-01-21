using DDD.Api.Common;
using DDD.Application.Clients.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly GetAllClientsQuery _query;

        public ClientsController(GetAllClientsQuery query)
        {
            _query = query;
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
    }
}
