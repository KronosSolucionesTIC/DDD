namespace DDD.Application.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string Generate(Domain.Entities.User user);
    }
}