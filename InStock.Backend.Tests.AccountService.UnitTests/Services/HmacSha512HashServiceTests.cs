using InStock.Backend.AccountService.Core.Services;
using InStock.Common.AccountService.Abstraction.Services;

namespace InStock.Backend.Tests.AccountService.UnitTests.Services
{
    internal class HmacSha512HashServiceTests
    {
        private IHashService _hashService;

        [SetUp]
        public void Setup()
        {
            _hashService = new HmacSha512HashService();
        }

        [Test]
        public void CreateHash_WhenCalled_ReturnsHashAndSalt()
        {
            // Arrange
            var password = "password";

            // Act
            _hashService.CreateHash(password, out var hash, out var salt);

            // Assert
            Assert.That(hash, Is.Not.Null);
            Assert.That(salt, Is.Not.Null);
        }

        [Test]
        public void VerifyHash_WhenCalled_ReturnsTrue()
        {
            // Arrange
            var password = "password";
            _hashService.CreateHash(password, out var hash, out var salt);

            // Act
            var result = _hashService.VerifyHash(password, hash, salt);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}