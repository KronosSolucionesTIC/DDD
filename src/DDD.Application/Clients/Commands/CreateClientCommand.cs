namespace DDD.Application.Clients.Commands.CreateClient;

public record CreateClientCommand(
    string Name,
    string Email
);
