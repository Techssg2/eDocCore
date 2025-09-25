using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDocCore.API.Controllers;
using eDocCore.Application.Roles.Commands;
using MediatR;

namespace eDocCore.Tests.Role
{
    public class RoleControllerUnitTests
    {
        [Fact]
        public async Task Create_Should_Return_CreatedAtActionResult()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new RoleController(mockMediator.Object);
            var command = new CreateRoleCommand { Name = "Admin" };
            var expectedId = Guid.NewGuid();

            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(expectedId);

            // Act
            var result = await controller.Create(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(command, createdResult.Value);
        }

        /*[Fact]
        public async Task Create_Should_Return_500_When_Exception()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new RoleController(mockMediator.Object);
            var command = new CreateRoleCommand { Name = "Admin" };
            mockMediator.Setup(m => m.Send(command, default)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await controller.Create(command);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Contains("DB error", statusResult.Value.ToString());
        }

        [Fact]
        public async Task Create_Should_Return_500_When_NameIsEmpty()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new RoleController(mockMediator.Object);
            var command = new CreateRoleCommand { Name = "" };
            mockMediator.Setup(m => m.Send(command, default)).ThrowsAsync(new ArgumentException("Name is required"));

            // Act
            var result = await controller.Create(command);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Contains("Name is required", statusResult.Value.ToString());
        }

        [Fact]
        public async Task Create_Should_Return_500_When_DuplicateName()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new RoleController(mockMediator.Object);
            var command = new CreateRoleCommand { Name = "Admin" };
            mockMediator.Setup(m => m.Send(command, default)).ThrowsAsync(new InvalidOperationException("Role already exists"));

            // Act
            var result = await controller.Create(command);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Contains("Role already exists", statusResult.Value.ToString());
        }

        [Fact]
        public async Task Create_Should_Return_500_When_RepositoryException()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new RoleController(mockMediator.Object);
            var command = new CreateRoleCommand { Name = "Admin" };
            mockMediator.Setup(m => m.Send(command, default)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await controller.Create(command);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Contains("DB error", statusResult.Value.ToString());
        }*/
    }
}
