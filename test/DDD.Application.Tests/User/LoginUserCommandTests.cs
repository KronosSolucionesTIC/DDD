using DDD.Application.Users.Commands;
using DDD.Application.Users.Dtos;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace DDD.Application.Test.Users
{
    public class LoginUserCommandTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly LoginUserCommand _command;

        public LoginUserCommandTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _command = new LoginUserCommand(_repositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldLoginSuccessfully_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "admin", "1234");

            _repositoryMock
                .Setup(r => r.GetByUsernameAsync("admin"))
                .ReturnsAsync(user);

            var request = new LoginRequestDto
            {
                Username = "admin",
                Password = "1234"
            };

            // Act
            var result = await _command.ExecuteAsync(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.CanAccessMenu.Should().BeTrue();
            result.Value.Username.Should().Be("admin");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldFail_WhenUserDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var request = new LoginRequestDto
            {
                Username = "admin",
                Password = "1234"
            };

            // Act
            var result = await _command.ExecuteAsync(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Usuario o contraseña inválidos");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldFail_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "admin", "1234");

            _repositoryMock
                .Setup(r => r.GetByUsernameAsync("admin"))
                .ReturnsAsync(user);

            var request = new LoginRequestDto
            {
                Username = "admin",
                Password = "wrong"
            };

            // Act
            var result = await _command.ExecuteAsync(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }
    }
}
