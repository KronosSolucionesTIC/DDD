namespace DDD.Application.Clients.Commands;

public record UpdateClientCommand(
    Guid ClientId,
    string Name,
    string Email
);
