namespace DDD.Application.Clients.Dtos;

public record ClientResponseDto(
    Guid Id,
    string Name,
    string Email,
    DateTime RegistrationDate
);