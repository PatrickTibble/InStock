using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.Login;
using Moq;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Dashboard
{
	public class MainPageModelTests
	{
        private Mock<INavigationService> _navigationService;
        private Mock<ISessionRepository> _sessionRepository;
        private MainPageModel _pageModel;

        [SetUp]
        public void Setup()
		{
            _navigationService = new Mock<INavigationService>();
            _sessionRepository = new Mock<ISessionRepository>();
            _pageModel = new MainPageModel(
                _navigationService.Object,
                _sessionRepository.Object);
		}

        [Test]
        public void Items_IsNotNull()
            => Assert.That(_pageModel.Items, Is.Not.Null);

        [Test]
        public void HeaderViewModel_IsNotNull()
            => Assert.That(_pageModel.HeaderViewModel, Is.Not.Null);

        [Test]
        public void Items_Selection_InvokesNavigation()
        {
            _ = _navigationService
                .Setup(n => n.NavigateToAsync<InventoryPageModel>(It.IsAny<object>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            _pageModel.Items!.First().Command.Execute(null);

            _navigationService
                .Verify(n => n.NavigateToAsync<InventoryPageModel>(It.IsAny<object>(), false), Times.Once);
        }

        [Test]
        public void Appearing_VerifiesUserSession()
        {
            var defaultState = Abstraction.Models.SessionState.Default;
            _ = _sessionRepository
                .Setup(s => s.GetSessionStateAsync())
                .Returns(Task.FromResult(defaultState));

            _pageModel
                .Appearing(null, new EventArgs());

            _sessionRepository
                .Verify(s => s.GetSessionStateAsync(), Times.Once);
        }

        [Test]
        public void Appearing_UserSessionInvalid_NavigatesToLogin()
        {
            var defaultState = Abstraction.Models.SessionState.Default;
            _ = _sessionRepository
                .Setup(s => s.GetSessionStateAsync())
                .Returns(Task.FromResult(defaultState));

            _ = _navigationService
                .Setup(n => n.NavigateToAsync<LoginPageModel>(It.IsAny<object>(), false))
                .Returns(Task.CompletedTask);

            _pageModel
                .Appearing(null, new EventArgs());

            _navigationService
                .Verify(n => n.NavigateToAsync<LoginPageModel>(It.IsAny<object>(), false), Times.Once);
        }

        [Test]
        public void Appearing_UserSessionValid_DoesNotNavigateToLogin()
        {
            var validState = new Abstraction.Models.SessionState
            {
                IsValid = true,
                SessionId = new Guid()
            };

            _ = _sessionRepository
                .Setup(s => s.GetSessionStateAsync())
                .Returns(Task.FromResult(validState));

            _ = _navigationService
                .Setup(n => n.NavigateToAsync<LoginPageModel>(It.IsAny<object>(), false))
                .Returns(Task.CompletedTask);

            _pageModel
                .Appearing(null, new EventArgs());

            _sessionRepository
                .Verify(s => s.GetSessionStateAsync(), Times.Once);

            _navigationService
                .Verify(n => n.NavigateToAsync<LoginPageModel>(It.IsAny<object>(), false), Times.Never);
        }
	}
}