using DDD.Domain.Entities;
using Moq;

namespace DDD.Application.Test.Navigation
{
    public class GetMenuQueryTests
    {
        private readonly Mock<IMenuRepository> _menuRepositoryMock;
        private readonly GetMenuQuery _useCase;

        public GetMenuQueryTests()
        {
            _menuRepositoryMock = new Mock<IMenuRepository>();
            _useCase = new GetMenuQuery(_menuRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnMenuItems_WhenRepositoryReturnsData()
        {
            // Arrange
            var menu = new List<MenuItem>
            {
                new MenuItem("Usuarios", "/users", "users", 1),
                new MenuItem("Roles", "/roles", "shield", 2)
            };

            _menuRepositoryMock
                .Setup(repo => repo.GetMenuAsync())
                .ReturnsAsync(menu);

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count());

            var firstItem = result.Value.First();
            Assert.Equal("Usuarios", firstItem.Title);
            Assert.Equal("/users", firstItem.Route);
            Assert.Equal("users", firstItem.Icon);

            _menuRepositoryMock.Verify(
                repo => repo.GetMenuAsync(),
                Times.Once
            );
        }
    }
}