using DDD.Application.Common;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;

namespace DDD.Application.Orders.Commands
{
    public class CreateOrderCommandHandler
    {
        private readonly IOrderRepository _repository;
        private readonly IClientRepository _clientRepository;

        public CreateOrderCommandHandler(IOrderRepository repository, IClientRepository clientRepository)
        {
            _repository = repository;
            _clientRepository = clientRepository;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand command)
        {
            if (!await _clientRepository.ExistsAsync(command.ClientId))
            {
                return Result<Guid>.Failure("El cliente no existe");
            }

            var order = new Order(command.ClientId, command.TotalAmount);

            await _repository.AddAsync(order);

            return Result<Guid>.Success(order.Id);
        }
    }
}
