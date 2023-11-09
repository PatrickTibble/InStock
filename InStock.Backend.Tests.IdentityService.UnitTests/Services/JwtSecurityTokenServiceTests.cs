using InStock.Backend.IdentityService.Core.Services;
using InStock.Common.Abstraction.Logger;
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using Microsoft.Extensions.Configuration;
using Moq;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Services
{
    internal class JwtSecurityTokenServiceTests
    {
        private ITokenService _tokenService;
        private Mock<ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger>();

            var section = new Mock<IConfigurationSection>();

            _ = section
                .Setup(x => x.Value)
                .Returns("This is a test key This is a test key This is a test key This is a test key This is a test key This is a test key");

            _ = configuration
                .Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(section.Object);


            _ = _logger
                .Setup(x => x.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _tokenService = new JwtSecurityTokenService(configuration.Object, _logger.Object);

        }

        [Test]
        public void CreateAccessRefreshTokenPair_WhenCalled_ReturnsAccessRefreshTokenPair()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            // Act
            var result = _tokenService.CreateAccessRefreshTokenPair(userToken);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<AccessRefreshTokenPair>());
                Assert.That(result!.AccessToken, Is.Not.Null);
                Assert.That(result.RefreshToken, Is.Not.Null);
                Assert.That(result.AccessToken, Is.Not.Empty);
                Assert.That(result.RefreshToken, Is.Not.Empty);
            });
        }

        [Test]
        public void CreateIdToken_WhenCalled_ReturnsIdToken()
        {
            // Arrange
            var username = "test";
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "username", username }
                }
            };

            // Act
            var result = _tokenService.CreateIdToken(userToken);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<string>());
                Assert.That(result, Is.Not.Empty);
            });
        }

        [Test]
        public void ReadToken_WhenCalled_ReturnsUserToken()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            var accessToken = _tokenService.CreateAccessRefreshTokenPair(userToken)!.AccessToken;

            // Act
            var result = _tokenService.ReadToken(accessToken);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<UserToken>());
                Assert.That(userToken.Expiry - result!.Expiry, Is.LessThan(TimeSpan.FromSeconds(1)));
            });
        }

        [Test]
        public void ReadToken_WhenCalledWithInvalidToken_ReturnsDefaultUserToken()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            var accessToken = _tokenService.CreateAccessRefreshTokenPair(userToken)!.AccessToken;

            // Act
            var result = _tokenService.ReadToken(accessToken + "invalid");

            // Assert
            Assert.That(result, Is.EqualTo(default));

            _logger.Verify(x => x.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RefreshWithTokenPair_WhenCalled_ReturnsAccessRefreshTokenPair()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            var tokenPair = _tokenService.CreateAccessRefreshTokenPair(userToken)!;

            // Act
            var result = _tokenService.RefreshWithTokenPair(tokenPair);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<AccessRefreshTokenPair>());
                Assert.That(result!.AccessToken, Is.Not.Null);
                Assert.That(result.RefreshToken, Is.Not.Null);
                Assert.That(result.AccessToken, Is.Not.Empty);
                Assert.That(result.RefreshToken, Is.Not.Empty);
            });
        }

        [Test]
        public void RefreshWithTokenPair_WhenCalledWithInvalidToken_ReturnsDefaultAccessRefreshTokenPair()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            var tokenPair = _tokenService.CreateAccessRefreshTokenPair(userToken)!;

            // Act
            var result = _tokenService.RefreshWithTokenPair(new AccessRefreshTokenPair
            {
                AccessToken = tokenPair.AccessToken + "invalid",
                RefreshToken = tokenPair.RefreshToken
            });

            // Assert
            Assert.That(result, Is.EqualTo(default));

            _logger.Verify(x => x.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ValidateToken_WhenCalled_ReturnsTrue()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            var accessToken = _tokenService.CreateAccessRefreshTokenPair(userToken)!.AccessToken;

            // Act
            var result = _tokenService.ValidateToken(accessToken);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateToken_WhenCalledWithExpiredToken_ReturnsFalseAndLogsException()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(-30)
            };

            var accessToken = _tokenService.CreateAccessRefreshTokenPair(userToken)!.AccessToken;

            // Act
            var result = _tokenService.ValidateToken(accessToken);

            // Assert
            Assert.That(result, Is.False);

            _logger.Verify(x => x.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ValidateToken_WhenCalledWithInvalidToken_ReturnsFalseAndLogsException()
        {
            // Arrange
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30)
            };

            var accessToken = _tokenService.CreateAccessRefreshTokenPair(userToken)!.AccessToken;

            // Act
            var result = _tokenService.ValidateToken(accessToken + "invalid");

            // Assert
            Assert.That(result, Is.False);

            _logger.Verify(x => x.LogExceptionAsync(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
    }
}