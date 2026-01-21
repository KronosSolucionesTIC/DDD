using DDD.Application.Clients.Dtos;
using DDD.Domain.Entities;

namespace DDD.Application.Clients.Mappers;

public static class ClientMapper
{
    public static ClientResponseDto ToDto(Client Client)
        => new(
            Client.Id,
            Client.Name,
            Client.Email,
            Client.RegistrationDate
        );
}
