using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Login;
using Moq;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Login
{
	public class LoginPageModelTests
	{
        private readonly Mock<INavigationService> _navigationService;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly LoginPageModel _pageModel;

        public LoginPageModelTests()
		{
            _navigationService = new Mock<INavigationService>();
            _sessionRepository = new Mock<ISessionRepository>();

            _pageModel = new LoginPageModel(
				_navigationService.Object,
				_sessionRepository.Object);
		}
	}
}