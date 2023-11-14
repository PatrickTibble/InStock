using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Abstraction.Services.Settings;
using InStock.Frontend.Core.PageModels.Login;
using Moq;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Login
{
	public class LoginPageModelTests
	{
        private Mock<IAlertService> _alertService;
        private Mock<IClientInfoService> _clientInfoService;
        private Mock<INavigationService> _navigationService;
        private Mock<IAccountManager> _accountRepository;
        private SoftwareVersion _version;
        private LoginPageModel _pageModel;

        [SetUp]
        public void Setup()
		{
            _alertService = new Mock<IAlertService>();
            _clientInfoService = new Mock<IClientInfoService>();
            _navigationService = new Mock<INavigationService>();
            _accountRepository = new Mock<IAccountManager>();

            _version = new SoftwareVersion(1, 1, 0, 1);
            _ = _clientInfoService.Setup(c => c.AppVersion).Returns(_version);

            _pageModel = new LoginPageModel(
				_navigationService.Object,
                _clientInfoService.Object,
                _alertService.Object,
				_accountRepository.Object);
		}

        [Test]
        public void UsernameViewModel_NotNull()
            => Assert.That(_pageModel.UsernameViewModel, Is.Not.Null);

        [Test]
        public void UsernameViewModel_Placeholder_NotNullOrEmpty()
            => Assert.Multiple(() =>
            {
                Assert.That(_pageModel.UsernameViewModel.Placeholder, Is.Not.Null);
                Assert.That(_pageModel.UsernameViewModel.Placeholder, Is.Not.Empty);
            });

        [Test]
        public void PasswordViewModel_NotNull()
            => Assert.That(_pageModel.PasswordViewModel, Is.Not.Null);

        [Test]
        public void PasswordViewModel_Placeholder_NotNullOrEmpty()
            => Assert.Multiple(() =>
            {
                Assert.That(_pageModel.PasswordViewModel.Placeholder, Is.Not.Null);
                Assert.That(_pageModel.PasswordViewModel.Placeholder, Is.Not.Empty);
            });

        [Test]
        public void PasswordViewModel_IsPasswordTrue()
            => Assert.That(_pageModel.PasswordViewModel.IsPassword, Is.True);

        [Test]
        public void LoginViewModel_NotNull()
            => Assert.That(_pageModel.LoginViewModel, Is.Not.Null);

        [Test]
        public void LoginViewModel_Command_CanExecuteTrue()
            => Assert.That(_pageModel.LoginViewModel.Command?.CanExecute(null), Is.True);

        [Test]
        public void LoginViewModel_Command_CanExecuteFalse()
        {
            _pageModel.IsLoading = true;
            Assert.That(_pageModel.LoginViewModel.Command?.CanExecute(null), Is.False);
        }

        [Test]
        public void LoginViewModel_Command_AttemptsLogin()
        {
            var loginResult = new BooleanResult();
            _ = _accountRepository
                .Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(loginResult));

            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.LoginViewModel.Command?.Execute(null);

            _accountRepository.Verify(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void LoginViewModel_Command_FailedLoginShowsAlert()
        {
            var loginResult = new BooleanResult();
            _ = _accountRepository
                .Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(loginResult));

            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.LoginViewModel.Command?.Execute(null);

            _alertService.Verify(a => a.ShowServiceAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AppVersion_IsSet()
            => Assert.That(_pageModel.AppVersion, Is.EqualTo(_version.ToString()));

        [Test]
        public void TryLogin_Fail_DoesNotNavigate()
        {
            var loginResult = new BooleanResult();
            _ = _accountRepository
                .Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(loginResult));

            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.LoginViewModel.Command?.Execute(null);

            _navigationService.Verify(n => n.PopAsync(), Times.Never);
        }

        [Test]
        public void TryLogin_Success_PopsNavigation()
        {
            var loginResult = new BooleanResult
            {
                Result = true
            };

            _ = _accountRepository
                .Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(loginResult));

            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.LoginViewModel.Command?.Execute(null);

            _accountRepository.Verify(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _navigationService.Verify(n => n.PopAsync(), Times.Once);
        }
	}
}