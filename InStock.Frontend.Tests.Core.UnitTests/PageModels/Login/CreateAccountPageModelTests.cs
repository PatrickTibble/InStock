using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Login;
using Moq;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Login
{
    public class CreateAccountPageModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IAlertService> _alertService;
        private Mock<IAccountRepository> _accountRepository;
        private CreateAccountPageModel _pageModel;

        private string _anyString => It.IsAny<string>();

        [SetUp]
        public void Setup()
        {
            _navigationService = new Mock<INavigationService>();
            _alertService = new Mock<IAlertService>();
            _accountRepository = new Mock<IAccountRepository>();

            _pageModel = new CreateAccountPageModel(
                navigationService: _navigationService.Object,
                accountRepository: _accountRepository.Object,
                alertService: _alertService.Object);
        }

        [Test]
        public void FirstNameViewModel_NotNull()
            => Assert.That(_pageModel.FirstNameViewModel, Is.Not.Null);

        [Test]
        public void FirstNameViewModel_Placeholder_NotNullOrEmpty()
            => AssertNotNullOrEmpty(_pageModel.FirstNameViewModel.Placeholder);

        [Test]
        public void LastNameViewModel_NotNull()
            => Assert.That(_pageModel.LastNameViewModel, Is.Not.Null);

        [Test]
        public void LastNameViewModel_Placeholder_NotNullOrEmpty()
            => AssertNotNullOrEmpty(_pageModel.LastNameViewModel.Placeholder);

        [Test]
        public void UsernameViewModel_NotNull()
            => Assert.That(_pageModel.UsernameViewModel, Is.Not.Null);

        [Test]
        public void UsernameViewModel_Placeholder_NotNullOrEmpty()
            => AssertNotNullOrEmpty(_pageModel.UsernameViewModel.Placeholder);

        [Test]
        public void PasswordViewModel_NotNull()
            => Assert.That(_pageModel.PasswordViewModel, Is.Not.Null);

        [Test]
        public void PasswordViewModel_Placeholder_NotNullOrEmpty()
            => AssertNotNullOrEmpty(_pageModel.PasswordViewModel.Placeholder);

        [Test]
        public void PasswordViewModel_IsPassword_True()
            => Assert.That(_pageModel.PasswordViewModel.IsPassword, Is.True);

        [Test]
        public void CancelViewModel_NotNull()
            => Assert.That(_pageModel.CancelViewModel, Is.Not.Null);

        [Test]
        public void CancelViewModel_Command_PopsNavigation()
        {
            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.CancelViewModel.Command?.Execute(null);

            _navigationService.Verify(n => n.PopAsync(), Times.Once);
        }

        [Test]
        public void ConfirmViewModel_NotNull()
            => Assert.That(_pageModel.ConfirmViewModel, Is.Not.Null);

        [Test]
        public void ConfirmViewModel_CreateAccountAsync_FailsAndAlerts()
        {
            var result = new CreateAccountResult();

            _ = _alertService
                .Setup(a => a.ShowServiceAlert(_anyString, _anyString, _anyString))
                .Returns(Task.CompletedTask);

            _ = _accountRepository
                .Setup(a => a.CreateAccountAsync(_anyString, _anyString, _anyString, _anyString))
                .Returns(Task.FromResult(result));

            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.ConfirmViewModel.Command?.Execute(null);

            _accountRepository.Verify(a => a.CreateAccountAsync(_anyString, _anyString, _anyString, _anyString), Times.Once);

            _alertService.Verify(a => a.ShowServiceAlert(_anyString, _anyString, _anyString), Times.Once);

            _navigationService.Verify(n => n.PopAsync(), Times.Never);
        }

        [Test]
        public void ConfirmViewModel_CreateAccountAsync_SucceedsAndNavigates()
        {
            var result = new CreateAccountResult
            {
                AccountCreationSuccessful = true
            };

            _ = _alertService
                .Setup(a => a.ShowServiceAlert(_anyString, _anyString, _anyString))
                .Returns(Task.CompletedTask);

            _ = _accountRepository
                .Setup(a => a.CreateAccountAsync(_anyString, _anyString, _anyString, _anyString))
                .Returns(Task.FromResult(result));

            _ = _navigationService
                .Setup(n => n.PopAsync())
                .Returns(Task.CompletedTask);

            _pageModel.ConfirmViewModel.Command?.Execute(null);

            _accountRepository.Verify(a => a.CreateAccountAsync(_anyString, _anyString, _anyString, _anyString), Times.Once);

            _alertService.Verify(a => a.ShowServiceAlert(_anyString, _anyString, _anyString), Times.Never);

            _navigationService.Verify(n => n.PopAsync(), Times.Once);
        }

        private void AssertNotNullOrEmpty(string? input)
            => Assert.Multiple(() =>
            {
                Assert.That(input, Is.Not.Null);
                Assert.That(input, Is.Not.Empty);
            });
    }
}