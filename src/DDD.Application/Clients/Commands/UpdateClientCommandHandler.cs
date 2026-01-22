using DDD.Domain.Repositories;

namespace DDD.Application.Clients.Commands
{
    public class UpdateClientCommandHandler
    {
        private readonly IClientRepository _repository;

        public UpdateClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateClientCommand command)
        {
            var client = await _repository.GetByIdAsync(command.ClientId);

            if (client is null)
                throw new ApplicationException("Cliente no encontrado");

            client.Update(command.Name, command.Email);

            await _repository.UpdateAsync(client);
        }
    }

}
