using InStock.Common.Abstraction.Logger;
using InStock.Common.AccountService.Abstraction.Entities;
using InStock.Common.AccountService.Abstraction.Repositories;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.Models.Base;
using Moq;

namespace InStock.Backend.Tests.AccountService.UnitTests.Services
{
    internal class AccountServiceTests
    {
        private Mock<IHashService> _hashService;
        private Mock<IIdentityService> _identityService;
        private Mock<IAccountRepository> _accountRepository;
        private Mock<ILogger> _logger;
        private Backend.AccountService.Core.Services.AccountService _service;

        [SetUp]
        public void Setup()
        {
            _hashService = new Mock<IHashService>();
            _identityService = new Mock<IIdentityService>();
            _accountRepository = new Mock<IAccountRepository>();
            _logger = new Mock<ILogger>();

            _service = new Backend.AccountService.Core.Services.AccountService(
                _hashService.Object,
                _identityService.Object,
                _accountRepository.Object,
                _logger.Object);
        }

        [Test]
        public async Task CreateAccountAsync_UsernameExists_ReturnsBadRequest()
        {
            // Arrange
            _ = _accountRepository
                .Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<UserAccount?>(new UserAccount()));

            var request = new CreateAccountRequest();

            // Act
            var result = await _service
                .CreateAccountAsync(request)
                .ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(400));
                Assert.That(result.Error, Is.Not.Empty);
            });
        }

        [Test]
        public async Task CreateAccountAsync_TokensNotReceived_ReturnsOkNoContent()
        {
            // Arrange
            _ = _accountRepository
                .Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<UserAccount?>(default));

            _ = _identityService
                .Setup(s => s.GetTokenAsync(It.IsAny<GetTokenRequest>()))
                .Returns(Task.FromResult(new Result<GetTokenResponse>(400, "no tokens")));

            var request = new CreateAccountRequest();

            // Act
            var result = await _service
                .CreateAccountAsync(request)
                .ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(201));
                Assert.That(result.Error, Is.Null);
            });
        }

        [Test]
        public async Task CreateAccountAsync_TokensReceived_ReturnsOk()
        {
            // Arrange
            _ = _accountRepository
                .Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<UserAccount?>(default));

            _ = _identityService
                .Setup(s => s.GetTokenAsync(It.IsAny<GetTokenRequest>()))
                .Returns(Task.FromResult(new Result<GetTokenResponse>(200, new GetTokenResponse())));

            var request = new CreateAccountRequest();

            // Act
            var result = await _service
                .CreateAccountAsync(request)
                .ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Data, Is.Not.Null);
                Assert.That(result.Error, Is.Null);
            });
        }
    }
}