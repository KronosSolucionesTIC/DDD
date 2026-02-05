using DDD.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly GetMenuQuery _query;

        public MenuController(GetMenuQuery query)
        {
            _query = query;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _query.ExecuteAsync();
            var response = ApiResponseMapper
                .FromResult(result, MenuMessages.GetMenuSuccess);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}