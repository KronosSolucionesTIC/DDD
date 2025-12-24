namespace DDD.Application.Users.Dtos;

public record UserResponseDto(
    Guid Id,
    string Name,
    string Email
);