using InStock.Backend.IdentityService.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Repositories
{
    internal class IdentityRepositoryTests
    {
        private Mock<IConfiguration> _configuration;
        private IdentityRepository _repository;

        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();

            _ = _configuration
                .Setup(config => config.GetSection("AppSettings.Token").Value)
                .Returns("ThisIsATestTokenThisIsATestTokenThisIsATestTokenThisIsATestTokenThisIsATestTokenThisIsATestTokenThisIsATestTokenThisIsATestToken");

            _repository = new IdentityRepository(_configuration.Object);
        }

        [Test]
        public async Task RegisterUserAsync_ReturnsTrue()
        {
            var result = await _repository.RegisterUserAsync("TestUser", "TestPassword");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task RegisterUserAsync_ReturnsFalse()
        {
            _ = await _repository.RegisterUserAsync("TestUser", "TestPassword");
            var result = await _repository.RegisterUserAsync("TestUser", "TestPassword");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetUserClaimsAsync_ReturnsClaims()
        {
            var accessToken = await GetValidToken();
            var claims = await _repository.GetUserClaimsAsync(accessToken!);

            Assert.Multiple(() =>
            {
                Assert.That(claims, Is.Not.Null);
                Assert.That(claims, Is.Not.Empty);
            });
        }

        private async Task<string?> GetValidToken()
        {
            _ = await _repository.RegisterUserAsync("TestUser", "TestPassword");
            var response = await _repository.VerifyUserCredentialsAsync("TestUser", "TestPassword", new List<string> { "session.read" });
            return response;
        }
    }
}