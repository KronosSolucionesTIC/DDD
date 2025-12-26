using DDD.Application.Users.Commands;
using DDD.Application.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUserCommand _loginUserCommand;

        public AuthController(LoginUserCommand loginUserCommand)
        {
            _loginUserCommand = loginUserCommand;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _loginUserCommand.ExecuteAsync(request);

            if (!result.IsSuccess)
                return Unauthorized(result.Error);

            return Ok(result.Value);
        }
    }
}
