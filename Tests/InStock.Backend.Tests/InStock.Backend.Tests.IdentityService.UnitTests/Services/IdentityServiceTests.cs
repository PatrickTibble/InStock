using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Exceptions;
using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using Moq;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Services
{
    internal class IdentityServiceTests
    {
        private Mock<ILogger> _logger;
        private Mock<ITokenService> _tokenService;
        private Mock<ITokenRepository> _tokenRepository;
        private Backend.IdentityService.Core.Services.IdentityService _identityService;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger>();
            _tokenService = new Mock<ITokenService>();
            _tokenRepository = new Mock<ITokenRepository>();

            _ = _logger
                .Setup(l => l.LogExceptionAsync(It.IsAny<CreateTokenException>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _identityService = new Backend.IdentityService.Core.Services.IdentityService(
                _tokenService.Object, 
                _tokenRepository.Object,
                _logger.Object);
        }

        #region GetTokenAsync
        [Test]
        public async Task GetTokenAsync_DefaultCreatedTokens_ReturnsBadRequest()
        {
            // Arrange
            SetupTokenService_CreateTokens(null, null, null);

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

            _logger.Verify(l => l.LogExceptionAsync(It.IsAny<CreateTokenException>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTokenAsync_ReturnsOk()
        {
            // Arrange
            SetupTokenService_CreateTokens();

            _ = _tokenRepository
                .Setup(repository => repository.SaveTokensAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

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
                Assert.That(result.Data!.AccessToken, Is.Not.Null);
                Assert.That(result.Data.IdentityToken, Is.Not.Null);
                Assert.That(result.Data.RefreshToken, Is.Not.Null);
            });
        }
        #endregion

        #region RefreshTokenAsync
        [TestCase(true, false)]
        [TestCase(true, true)]
        [TestCase(false, true)]
        public async Task RefreshTokenAsync_TokensFromTokenService_ReturnsBadRequest(bool tokenServiceDefaults, bool tokenRepoDefaults)
        {
            // Arrange
            SetupTokenService_ReadTokens(tokenServiceDefaults);
            SetupTokenRepository_GetTokens(tokenRepoDefaults);

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

            _logger.Verify(l => l.LogExceptionAsync(It.IsAny<CreateTokenException>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task RefreshTokenAsync_InvalidatedToken_InvalidatesTokenFamily()
        {
            SetupTokenService_ReadTokens();
            SetupTokenRepository_GetTokens(invalidated: true);

            _ = _tokenRepository
                .Setup(repo => repo.InvalidateTokenFamilyAsync(It.IsAny<StoredRefreshToken>()))
                .Returns(Task.FromResult(true));

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

            _tokenRepository.Verify(repo => repo.InvalidateTokenFamilyAsync(It.IsAny<StoredRefreshToken>()), Times.Once);

            _logger.Verify(l => l.LogExceptionAsync(It.IsAny<CreateTokenException>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task RefreshTokenAsync_InvalidatesPreviousTokens_ReturnsNewTokens()
        {
            SetupTokenService_ReadTokens();
            SetupTokenRepository_GetTokens();
            SetupTokenService_CreateTokens();

            _ = _tokenRepository
                .Setup(repo => repo.SaveTokensAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            _ = _tokenRepository
                .Setup(repo => repo.InvalidateTokensAsync(It.IsAny<StoredRefreshToken>(), It.IsAny<StoredAccessToken>()))
                .Returns(Task.FromResult(true));

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
                Assert.That(result.Data!.AccessToken, Is.Not.Null);
                Assert.That(result.Data.RefreshToken, Is.Not.Null);
            });
        }
        #endregion

        #region ValidateTokenAsync

        #endregion

        #region private Setup Methods
        private void SetupTokenService_CreateTokens(
            string? accessTokenResult = "accessToken",
            string? refreshTokenResult = "refreshToken",
            string? idTokenResult = "idToken")
        {
            _ = _tokenService
                .Setup(service => service.CreateAccessTokenAsync())
                .Returns(Task.FromResult(accessTokenResult));

            _ = _tokenService
                .Setup(service => service.CreateRefreshTokenAsync())
                .Returns(Task.FromResult(refreshTokenResult));

            _ = _tokenService
                .Setup(service => service.CreateIdentityTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(idTokenResult));
        }

        private void SetupTokenService_ReadTokens(bool nullResult = false)
        {
            _ = _tokenService
                .Setup(service => service.ReadTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(nullResult ? default : new UserToken()));
        }

        private void SetupTokenRepository_GetTokens(bool nullResult = false, bool invalidated = false)
        {
            _ = _tokenRepository
                .Setup(repository => repository.GetRefreshTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(nullResult ? default : new StoredRefreshToken() { Invalidated = invalidated }));

            _ = _tokenRepository
                .Setup(repository => repository.GetAccessTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(nullResult ? default : new StoredAccessToken() { Invalidated = invalidated }));

            _ = _tokenRepository
                .Setup(repository => repository.GetIdentityTokenAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(nullResult ? default : new StoredToken() { Invalidated = invalidated }));

            _ = _tokenRepository
                .Setup(repository => repository.GetIdentityTokenAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(nullResult ? default : new StoredToken() { Invalidated = invalidated }));
        }
        #endregion
    }
}