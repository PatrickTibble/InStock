using InStock.Backend.AccountService.Controllers;
using InStock.Common.AccountService.Abstraction.Services;
using Moq;

namespace InStock.Backend.Tests.AccountService.UnitTests.Controllers
{
    internal class UserControllerTests
    {
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            var userService = new Mock<IUserService>();

            _controller = new UserController(
                userService.Object);
        }
    }
}