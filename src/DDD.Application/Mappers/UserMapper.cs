using DDD.Application.Users.Dtos;
using DDD.Domain.Entities;

namespace DDD.Application.Users.Mappers;

public static class UserMapper
{
    public static UserResponseDto ToDto(User user)
        => new(
            user.Id,
            user.UserName,
            user.PassWordHash
        );
}
