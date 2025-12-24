using DDD.Api.Common;
using DDD.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly GetAllUsersQuery _query;

        public UsersController(GetAllUsersQuery query)
        {
            _query = query;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _query.ExecuteAsync();

            var response = ApiResponseMapper
                .FromResult(result, "Usuarios obtenidos correctamente");

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
