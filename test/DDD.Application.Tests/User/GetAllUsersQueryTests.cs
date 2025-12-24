using DDD.Application.Users.Dtos;
using DDD.Application.Users.Queries;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace DDD.Application.Test.Users
{
    public class GetAllUsersQueryTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly GetAllUsersQuery _useCase;

        public GetAllUsersQueryTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _useCase = new GetAllUsersQuery(_repositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnAllUsersMappedToDto()
        {
            // Arrange
            var users = new List<User>
            {
                new User(Guid.NewGuid(), "Alejandro", "alejo@test.com"),
                new User(Guid.NewGuid(), "Laura", "laura@test.com")
            };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
            result.Value.Should().AllBeOfType<UserResponseDto>();

            result.Value.First().Name.Should().Be("Alejandro");

            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>());

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty();
        }
    }
}
