using DDD.Application.Common.Constants;
using DDD.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Authorize]
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
                .FromResult(result, UserMessages.GetUsersSuccess);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
