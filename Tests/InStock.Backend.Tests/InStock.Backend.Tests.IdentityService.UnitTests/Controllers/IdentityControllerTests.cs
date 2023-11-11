using InStock.Backend.IdentityService.Controllers;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using Microsoft.AspNetCore.Mvc;
using Base = InStock.Common.Models.Base;
using Moq;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Controllers
{
    internal class IdentityControllerTests
    {
        private Mock<IIdentityService> _identityService;
        private IdentityController _controller;

        [SetUp]
        public void Setup()
        {
            _identityService = new Mock<IIdentityService>();

            _controller = new IdentityController(
                _identityService.Object);
        }

        [Test]
        public async Task GetTokenAsync_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller
                .GetTokenAsync(new GetTokenRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetTokenAsync_ValidModel_ReturnsOk()
        {
            // Arrange
            _identityService
                .Setup(x => x.GetTokenAsync(It.IsAny<GetTokenRequest>()))
                .ReturnsAsync(new Base.Result<GetTokenResponse>(new GetTokenResponse()));

            // Act
            var result = await _controller
                .GetTokenAsync(new GetTokenRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task ValidateTokenAsync_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller
                .ValidateTokenAsync(new ValidateTokenRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task ValidateTokenAsync_ValidModel_ReturnsOk()
        {
            // Arrange
            _identityService
                .Setup(x => x.ValidateTokenAsync(It.IsAny<ValidateTokenRequest>()))
                .ReturnsAsync(new Base.Result<ValidateTokenResponse>(new ValidateTokenResponse()));

            // Act
            var result = await _controller
                .ValidateTokenAsync(new ValidateTokenRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task RefreshTokenAsync_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller
                .RefreshTokenAsync(new AccessRefreshTokenPair())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task RefreshTokenAsync_ValidModel_ReturnsOk()
        {
            // Arrange
            _identityService
                .Setup(x => x.RefreshTokenAsync(It.IsAny<AccessRefreshTokenPair>()))
                .ReturnsAsync(new Base.Result<AccessRefreshTokenPair>(new AccessRefreshTokenPair()));

            // Act
            var result = await _controller
                .RefreshTokenAsync(new AccessRefreshTokenPair())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}