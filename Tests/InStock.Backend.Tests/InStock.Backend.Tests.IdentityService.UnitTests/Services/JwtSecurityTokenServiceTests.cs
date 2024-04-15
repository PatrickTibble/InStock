using InStock.Backend.IdentityService.Core.Services;
using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.Services;
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
        public void CreateAccessTokenAsync_ReturnsAccessToken()
            => Assert.That(_tokenService.CreateAccessTokenAsync().Result, Is.Not.Null);

        [Test]
        public void CreateIdentityTokenAsync_ReturnsIdentityToken()
            => Assert.That(_tokenService.CreateIdentityTokenAsync("test").Result, Is.Not.Null);

        [Test]
        public void CreateRefreshTokenAsync_ReturnsRefreshToken()
            => Assert.That(_tokenService.CreateRefreshTokenAsync().Result, Is.Not.Null);

        [Test]
        public async Task ReadTokenAsync_ReturnsUserToken()
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "access" }
                }
            };

            var token = await _tokenService.CreateTokenAsync(userToken);

            var result = await _tokenService.ReadTokenAsync(token!);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(userToken.Expiry - result!.Expiry, Is.LessThan(TimeSpan.FromSeconds(1)));
                Assert.That(result.Claims, Is.Not.Null);
                Assert.That(result.Claims!["typ"], Is.EqualTo(userToken.Claims["typ"]));
            });
        }

        [Test]
        public async Task ReadTokenAsync_ThrowsException_WhenTokenIsInvalid()
        {
            var result = await _tokenService.ReadTokenAsync("invalid token");

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task ReadTokenAsync_ThrowsException_WhenTokenIsExpired()
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(-30),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "access" }
                }
            };

            var token = await _tokenService.CreateTokenAsync(userToken);

            var result = await _tokenService.ReadTokenAsync(token!);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task ReadTokenAsync_ThrowsException_WhenTokenSignatureIsNotValid()
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "access" }
                }
            };

            var token = await _tokenService.CreateTokenAsync(userToken);

            var result = await _tokenService.ReadTokenAsync(token! + "invalid");

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task ReadTokenAsync_ThrowsException_WhenTokenMalformed()
        {
            var userToken = new UserToken
            {
                Expiry = DateTime.UtcNow.AddMinutes(30),
                Claims = new Dictionary<string, string>
                {
                    { "typ", "access" }
                }
            };

            var token = await _tokenService.CreateTokenAsync(userToken);

            var result = await _tokenService.ReadTokenAsync(token!.Replace('.', ' '));

            Assert.That(result, Is.Null);
        }
    }
}