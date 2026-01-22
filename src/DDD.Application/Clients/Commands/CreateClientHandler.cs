using DDD.Application.Common;
using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Repositories;

namespace DDD.Application.Clients.Commands.CreateClient;

public class CreateClientCommandHandler
{
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Result<Guid>> Handle(CreateClientCommand command)
    {
        try
        {
            var client = new Client(command.Name, command.Email);
            await _clientRepository.AddAsync(client);

            return Result<Guid>.Success(client.Id);
        }
        catch (DomainException ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
    }
}
