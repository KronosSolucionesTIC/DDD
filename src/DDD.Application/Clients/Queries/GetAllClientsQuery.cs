using DDD.Application.Common;
using DDD.Application.Clients.Dtos;
using DDD.Application.Clients.Mappers;
using DDD.Domain.Repositories;

namespace DDD.Application.Clients.Queries
{
    public class GetAllClientsQuery
    {
        private readonly IClientRepository _repository;

        public GetAllClientsQuery(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<ClientResponseDto>>> ExecuteAsync()
        {
            var Clients = await _repository.GetAllAsync();

            var dtoList = Clients.Select(ClientMapper.ToDto);

            return Result<IEnumerable<ClientResponseDto>>
                .Success(dtoList);
        }
    }
}
