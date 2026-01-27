using DDD.Application.Clients.Dtos;
using DDD.Application.Clients.Mappers;
using DDD.Application.Common;
using DDD.Domain.Repositories;

namespace DDD.Application.Clients.Queries
{
    public class GetClient
    {
        private readonly IClientRepository _repository;
        public GetClient(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ClientResponseDto>> ExecuteAsync(Guid clientId)
        {
            var client = await _repository.GetByIdAsync(clientId);
            if (client == null)
            {
                return Result<ClientResponseDto>
                    .Failure("Cliente no existe");
            }
            var dto = ClientMapper.ToDto(client);
            return Result<ClientResponseDto>
                .Success(dto);
        }
    }
}
