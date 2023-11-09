using InStock.Common.Abstraction.Logger;
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken;
using Moq;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Services
{
    internal class IdentityServiceTests
    {
        private Mock<ILogger> _logger;
        private Mock<IIdentityRepository> _identityRepository;
        private Mock<ITokenService> _tokenService;
        private Backend.IdentityService.Core.Services.IdentityService _identityService;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger>();
            _identityRepository = new Mock<IIdentityRepository>();
            _tokenService = new Mock<ITokenService>();
            
            _identityService = new Backend.IdentityService.Core.Services.IdentityService(
                _identityRepository.Object, 
                _tokenService.Object, 
                _logger.Object);
        }

        #region GetTokenAsynnc Tests
        [Test]
        public async Task GetTokenAsync_DefaultTokenPair_ReturnsBadRequest()
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.CreateAccessRefreshTokenPair(It.IsAny<UserToken>()))
                .Returns(default(AccessRefreshTokenPair));

            var request = new GetTokenRequest();

            // Act
            var result = await _identityService
                .GetTokenAsync(request)
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
        public async Task GetTokenAsync_DefaultIdToken_ReturnsBadRequest()
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.CreateAccessRefreshTokenPair(It.IsAny<UserToken>()))
                .Returns(new AccessRefreshTokenPair());

            _ = _tokenService
                .Setup(service => service.CreateIdToken(It.IsAny<string>(), It.IsAny<AccessRefreshTokenPair>()))
                .Returns(default(string));

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(default(Token));

            var request = new GetTokenRequest();

            // Act
            var result = await _identityService
                .GetTokenAsync(request)
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
        public async Task GetTokenAsync_DefaultSaveResult_ReturnsBadRequest()
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.CreateAccessRefreshTokenPair(It.IsAny<UserToken>()))
                .Returns(new AccessRefreshTokenPair());

            _ = _tokenService
                .Setup(service => service.CreateIdToken(It.IsAny<string>(), It.IsAny<AccessRefreshTokenPair>()))
                .Returns("idToken");

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(default(Token));

            _ = _identityRepository
                .Setup(repository => repository.StoreTokensAsync(It.IsAny<string>(), It.IsAny<AccessRefreshTokenPair>()))
                .ReturnsAsync(false);

            var request = new GetTokenRequest();

            // Act
            var result = await _identityService
                .GetTokenAsync(request)
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
        public async Task GetTokenAsync_ValidRequest_ReturnsTokenPair()
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.CreateAccessRefreshTokenPair(It.IsAny<UserToken>()))
                .Returns(new AccessRefreshTokenPair());

            _ = _tokenService
                .Setup(service => service.CreateIdToken(It.IsAny<string>(), It.IsAny<AccessRefreshTokenPair>()))
                .Returns("idToken");

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(default(Token));

            _ = _identityRepository
                .Setup(repository => repository.StoreTokensAsync(It.IsAny<string>(), It.IsAny<AccessRefreshTokenPair>()))
                .ReturnsAsync(true);

            var request = new GetTokenRequest();

            // Act
            var result = await _identityService
                .GetTokenAsync(request)
                .ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Error, Is.Null);
                Assert.That(result.Data, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetTokenAsync_ExceptionGetsLogged()
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.CreateAccessRefreshTokenPair(It.IsAny<UserToken>()))
                .Throws<Exception>();

            _ = _logger
                .Setup(logger => logger.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var request = new GetTokenRequest();

            // Act
            var result = await _identityService
                .GetTokenAsync(request)
                .ConfigureAwait(false);

            // Assert
            _logger.Verify(logger => logger.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region RefreshTokenAsync Tests
        [Test]
        public async Task RefreshTokenAsync_InvalidTokenPair_ReturnsBadRequest()
        {
            // Arrange
            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenPairAsync(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(Task.FromResult(false));

            var request = new AccessRefreshTokenPair();

            // Act
            var result = await _identityService
                .RefreshTokenAsync(request)
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
        public async Task RefreshTokenAsync_InvalidIdToken_ReturnsBadRequest()
        {
            // Arrange
            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenPairAsync(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(Task.FromResult(true));

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(default(Token));

            var request = new AccessRefreshTokenPair();

            // Act
            var result = await _identityService
                .RefreshTokenAsync(request)
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
        public async Task RefreshTokenAsync_InvalidRefreshResult_ReturnsBadRequest()
        {
            // Arrange
            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenPairAsync(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(Task.FromResult(true));

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new Token());

            _ = _tokenService
                .Setup(service => service.RefreshWithTokenPair(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(default(AccessRefreshTokenPair));

            var request = new AccessRefreshTokenPair();

            // Act
            var result = await _identityService
                .RefreshTokenAsync(request)
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
        public async Task RefreshTokenAsync_InvalidSaveResult_ReturnsBadRequest()
        {
            // Arrange
            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenPairAsync(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(Task.FromResult(true));

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new Token());

            _ = _tokenService
                .Setup(service => service.RefreshWithTokenPair(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(new AccessRefreshTokenPair());

            _ = _identityRepository
                .Setup(repository => repository.SaveTokenPairAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var request = new AccessRefreshTokenPair();

            // Act
            var result = await _identityService
                .RefreshTokenAsync(request)
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
        public async Task RefreshTokenAsync_ValidRequest_ReturnsTokenPair()
        {
            // Arrange
            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenPairAsync(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(Task.FromResult(true));

            _ = _identityRepository
                .Setup(repository => repository.GetIdTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new Token
                {
                    TokenValue = "idToken"
                });

            _ = _tokenService
                .Setup(service => service.RefreshWithTokenPair(It.IsAny<AccessRefreshTokenPair>()))
                .Returns(new AccessRefreshTokenPair());

            _ = _identityRepository
                .Setup(repository => repository.SaveTokenPairAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var request = new AccessRefreshTokenPair();

            // Act
            var result = await _identityService
                .RefreshTokenAsync(request)
                .ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Error, Is.Null);
                Assert.That(result.Data, Is.Not.Null);
            });
        }

        [Test]
        public async Task RefreshTokenAsync_ExceptionGetsLogged()
        {
            // Arrange
            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenPairAsync(It.IsAny<AccessRefreshTokenPair>()))
                .Throws<Exception>();

            _ = _logger
                .Setup(logger => logger.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var request = new AccessRefreshTokenPair();

            // Act
            var result = await _identityService
                .RefreshTokenAsync(request)
                .ConfigureAwait(false);

            // Assert
            _logger.Verify(logger => logger.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region ValidateTokenAsync Tests
        [TestCase(false, true)]
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public async Task ValidateTokenAsync(bool validToken, bool savedInRepo)
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.ValidateToken(It.IsAny<string>()))
                .Returns(validToken);

            _ = _identityRepository
                .Setup(repository => repository.ValidateTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(savedInRepo);

            var request = new ValidateTokenRequest();

            // Act
            var result = await _identityService
                .ValidateTokenAsync(request)
                .ConfigureAwait(false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Data, Is.Not.Null);
                Assert.That(result.Data!.IsValid, Is.EqualTo(validToken && savedInRepo));
            });
        }

        [Test]
        public async Task ValidateTokenAsync_ExceptionGetsLogged()
        {
            // Arrange
            _ = _tokenService
                .Setup(service => service.ValidateToken(It.IsAny<string>()))
                .Throws<Exception>();

            _ = _logger
                .Setup(logger => logger.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var request = new ValidateTokenRequest();

            // Act
            var result = await _identityService
                .ValidateTokenAsync(request)
                .ConfigureAwait(false);

            // Assert
            _logger.Verify(logger => logger.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
        #endregion
    }
}