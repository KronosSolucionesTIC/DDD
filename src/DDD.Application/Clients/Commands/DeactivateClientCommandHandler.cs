using DDD.Domain.Repositories;

namespace DDD.Application.Clients.Commands.DeactivateClient;

public class DeactivateClientCommandHandler
{
    private readonly IClientRepository _repository;

    public DeactivateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeactivateClientCommand command)
    {
        var client = await _repository.GetByIdAsync(command.ClientId);

        if (client is null)
            throw new ApplicationException("Cliente no encontrado");

        client.Deactivate();

        await _repository.UpdateAsync(client);
    }
}
