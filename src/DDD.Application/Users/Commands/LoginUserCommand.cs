using DDD.Application.Common;
using DDD.Application.Users.Dtos;
using DDD.Domain.Repositories;

namespace DDD.Application.Users.Commands
{
    public class LoginUserCommand
    {
        private readonly IUserRepository _repository;

        public LoginUserCommand(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<LoginResponseDto>> ExecuteAsync(LoginRequestDto request)
        {
            var user = await _repository.GetByUsernameAsync(request.Username);

            if (user is null)
                return Result<LoginResponseDto>.Failure("Usuario o contraseña inválidos");

            if (!user.ValidatePassword(request.Password))
                return Result<LoginResponseDto>.Failure("Usuario o contraseña inválidos");

            return Result<LoginResponseDto>.Success(new LoginResponseDto
            {
                UserId = user.Id,
                Username = user.UserName,
                CanAccessMenu = true
            });
        }
    }
}
