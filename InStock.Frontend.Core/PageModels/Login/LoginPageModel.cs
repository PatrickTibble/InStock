using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Input;

namespace InStock.Frontend.Core.PageModels.Login
{
    public partial class LoginPageModel : BasePageModel
	{
        private readonly IAlertService _alertService;
        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string? _appVersion;

        public LoginPageModel(
            INavigationService navigationService,
            IClientInfoService infoService,
            IAlertService alertService,
            IAccountRepository accountRepository)
		{
            _alertService = alertService;
            _navigationService = navigationService;
            _accountRepository = accountRepository;

            AppVersion = infoService.AppVersion.ToString();

            UsernameViewModel = new PrimaryEntryViewModel
            {
                Placeholder = Strings.Placeholder_Username
            };

            PasswordViewModel = new PrimaryEntryViewModel
            {
                Placeholder = Strings.Placeholder_Password,
                IsPassword = true
            };

            LoginViewModel = new ButtonViewModel
            {
                Command = new AsyncRelayCommand(TryLoginWithCredentialsAsync, () => !IsLoading),
                Title = Strings.ButtonTitle_Login
            };

            CreateAccountViewModel = new ButtonViewModel
            {
                Command = new AsyncRelayCommand(TryNavigateToCreateAccountAsync, () => !IsLoading),
                Title = Strings.ButtonTitle_CreateAccount
            };
        }

        public PrimaryEntryViewModel UsernameViewModel { get; }

        public PrimaryEntryViewModel PasswordViewModel { get; }

        public ButtonViewModel LoginViewModel { get; }

        public ButtonViewModel CreateAccountViewModel { get; }

        private async Task TryLoginWithCredentialsAsync()
        {
            IsLoading = true;
            var loginResult = await _accountRepository
                .LoginAsync(UsernameViewModel.Text, PasswordViewModel.Text)
                .ConfigureAwait(false);

            if (loginResult.IsSuccessful)
            {
                await _navigationService.PopAsync().ConfigureAwait(false);
                return;
            }

            IsLoading = false;
            await _alertService
                .ShowServiceAlert(Strings.AlertTitle_LoginFailed, Strings.AlertBody_LoginFailed, Strings.AlertAction_Confirm)
                .ConfigureAwait(false);
        }

        private Task TryNavigateToCreateAccountAsync()
            => _navigationService.NavigateToAsync<CreateAccountPageModel>();
    }
}