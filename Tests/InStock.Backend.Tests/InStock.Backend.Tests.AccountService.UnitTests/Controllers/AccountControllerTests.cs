using InStock.Backend.AccountService.Controllers;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using Microsoft.AspNetCore.Mvc;
using Base = InStock.Common.Models.Base;
using Moq;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;

namespace InStock.Backend.Tests.AccountService.UnitTests.Controllers
{
    internal class AccountControllerTests
    {
        private Mock<IAccountService> _accountService;
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            _accountService = new Mock<IAccountService>();

            _controller = new AccountController(
                _accountService.Object);
        }

        [Test]
        public async Task LoginAsync_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller
                .LoginAsync(new LoginRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task LoginAsync_ValidModel_ReturnsOk()
        {
            // Arrange
            _accountService
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync(new Base.Result<LoginResponse>(new LoginResponse()));

            // Act
            var result = await _controller
                .LoginAsync(new LoginRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task CreateUserAccountAsync_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await _controller
                .CreateUserAccountAsync(new CreateAccountRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateUserAccountAsync_ValidModel_ReturnsOk()
        {
            // Arrange
            _accountService
                .Setup(x => x.CreateAccountAsync(It.IsAny<CreateAccountRequest>()))
                .ReturnsAsync(new Base.Result<LoginResponse>(new LoginResponse()));

            // Act
            var result = await _controller
                .CreateUserAccountAsync(new CreateAccountRequest())
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}