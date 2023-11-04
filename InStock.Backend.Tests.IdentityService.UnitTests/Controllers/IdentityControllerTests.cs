using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Controllers;
using Moq;

namespace InStock.Backend.Tests.IdentityService.UnitTests.Controllers
{
    internal class IdentityControllerTests
    {
        private Mock<IIdentityService> _identityService;
        private IdentityController _controller;

        public void Setup()
        {
            _identityService = new Mock<IIdentityService>();
            _controller = new IdentityController(
                _identityService.Object);
        }
    }
}