using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Backend.IdentityService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Controllers
{
    internal class IdentityControllerTests
    {
        private Mock<IIdentityService> _identityService;
        private IdentityController _controller;

        private AuthenticationRequest _authentication_GoodRequest = new AuthenticationRequest()
        {
            Username = "something_longer_than_8_characters",
            Password = "also_something_longer_than_8_characters"
        };

        private AuthenticationRequest _authentication_BadRequest = new AuthenticationRequest()
        {
            Username = "short",
            Password = "short"
        };

        private RegistrationRequest _registration_GoodRequest = new RegistrationRequest()
        {
            Username = "something_longer_than_8_characters",
            Password = "also_something_longer_than_8_characters"
        };

        private RegistrationRequest _registration_BadRequest = new RegistrationRequest()
        {
            Username = "short",
            Password = "short"
        };

        [SetUp]
        public void Setup()
        {
            
            _identityService = new Mock<IIdentityService>();

            _ = _identityService
                .Setup(s => s.AuthenticateAsync(_authentication_GoodRequest))
                .ReturnsAsync(new AuthenticationResponse()
                {
                    AccessToken = "token"
                });

            _ = _identityService
                .Setup(s => s.AuthenticateAsync(_authentication_BadRequest))
                .ReturnsAsync(new AuthenticationResponse()
                {
                    AccessToken = null
                });

            _controller = new IdentityController(
                _identityService.Object);
        }

        [Test]
        public async Task AuthenticateAsync_WhenCalled_ReturnsOk()
        {
            // Arrange
            // Act
            var result = await _controller.AuthenticateAsync(_authentication_GoodRequest);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task AuthenticateAsync_WhenCalled_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller.AuthenticateAsync(_authentication_BadRequest);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task RegisterAsync_WhenCalled_ReturnsOk()
        {
            // Arrange
            // Act
            var result = await _controller.RegisterAsync(_registration_GoodRequest);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task RegisterAsync_WhenCalled_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller.RegisterAsync(_registration_BadRequest);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}