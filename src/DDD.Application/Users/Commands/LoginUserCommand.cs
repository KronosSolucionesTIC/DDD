using DDD.Application.Common;
using DDD.Application.Common.Constants;
using DDD.Application.Common.Security;
using DDD.Application.Users.Dtos;
using DDD.Domain.Repositories;

namespace DDD.Application.Users.Commands
{
    public class LoginUserCommand
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommand(IUserRepository repository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _repository = repository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<LoginResponseDto>> ExecuteAsync(LoginRequestDto request)
        {
            var user = await _repository.GetByUsernameAsync(request.Username);

            if (user is null)
                return Result<LoginResponseDto>.Failure(AuthMessages.InvalidData);

            if (!user.ValidatePassword(request.Password))
                return Result<LoginResponseDto>.Failure(AuthMessages.InvalidData);

            var token = _jwtTokenGenerator.Generate(user);

            return Result<LoginResponseDto>.Success(new LoginResponseDto
            {
                UserId = user.Id,
                Username = user.UserName,
                CanAccessMenu = true,
                Token = token
            });
        }
    }
}
