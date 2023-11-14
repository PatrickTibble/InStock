using InStock.Frontend.Abstraction.Managers;
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
        private Mock<ISessionManager> _sessionManager;
        private MainPageModel _pageModel;

        [SetUp]
        public void Setup()
		{
            _navigationService = new Mock<INavigationService>();
            _sessionManager = new Mock<ISessionManager>();
            _pageModel = new MainPageModel(
                _navigationService.Object,
                _sessionManager.Object);
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
            _ = _sessionManager
                .Setup(s => s.ValidateSessionAsync())
                .Returns(Task.FromResult(false));

            _pageModel
                .Appearing(null, new EventArgs());

            _sessionManager
                .Verify(s => s.ValidateSessionAsync(), Times.Once);
        }

        [Test]
        public void Appearing_UserSessionInvalid_NavigatesToLogin()
        {
            _ = _sessionManager
                .Setup(s => s.ValidateSessionAsync())
                .Returns(Task.FromResult(false));

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
            _ = _sessionManager
                .Setup(s => s.ValidateSessionAsync())
                .Returns(Task.FromResult(true));

            _ = _navigationService
                .Setup(n => n.NavigateToAsync<LoginPageModel>(It.IsAny<object>(), false))
                .Returns(Task.CompletedTask);

            _pageModel
                .Appearing(null, new EventArgs());

            _sessionManager
                .Verify(s => s.ValidateSessionAsync(), Times.Once);

            _navigationService
                .Verify(n => n.NavigateToAsync<LoginPageModel>(It.IsAny<object>(), false), Times.Never);
        }
	}
}