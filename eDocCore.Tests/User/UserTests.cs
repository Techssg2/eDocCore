using Xunit;
using eDocCore.Application.Users.Commands;
using eDocCore.Application.Users.Handlers;
using Moq;
using eDocCore.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace eDocCore.Tests.User
{
    public class UserTests
    {
        [Fact]
        public async Task CreateUserCommandHandler_Should_Create_User()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var handler = new CreateUserCommandHandler(mockRepo.Object);
            var command = new CreateUserCommand
            {
                LoginName = "testuser",
                FullName = "Test User",
                Gender = true,
                Email = "test@example.com",
                IsActive = true
            };
            var expectedId = Guid.NewGuid();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<eDocCore.Domain.Entities.User>())).ReturnsAsync(expectedId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedId, result);
        }
    }
}
