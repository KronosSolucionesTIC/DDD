using DDD.Application.Common;
using DDD.Application.Users.Dtos;
using DDD.Application.Users.Mappers;
using DDD.Domain.Repositories;

namespace DDD.Application.Users.Queries
{
    public class GetAllUsersQuery
    {
        private readonly IUserRepository _repository;

        public GetAllUsersQuery(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<UserResponseDto>>> ExecuteAsync()
        {
            var users = await _repository.GetAllAsync();

            var dtoList = users.Select(UserMapper.ToDto);

            return Result<IEnumerable<UserResponseDto>>
                .Success(dtoList);
        }
    }
}
