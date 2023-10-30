using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Input;

namespace InStock.Frontend.Core.PageModels.Login
{
    public partial class LoginPageModel : BasePageModel
	{
        private readonly INavigationService _navigationService;
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountRepository _accountRepository;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string? _appVersion;

        public LoginPageModel(
            INavigationService navigationService,
            IClientInfoService infoService,
            ISessionRepository sessionRepository,
            IAccountRepository accountRepository)
		{
            _navigationService = navigationService;
            _sessionRepository = sessionRepository;
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
        }

        public PrimaryEntryViewModel UsernameViewModel { get; }

        public PrimaryEntryViewModel PasswordViewModel { get; }

        public ButtonViewModel LoginViewModel { get; }

        public override async Task InitializeAsync(object? navigationData = null)
        {
            IsLoading = true;
            await base.InitializeAsync(navigationData);
            // Determine if user is logged in and if current
            // session is active.
            var sessionStatus = await _sessionRepository.GetSessionStateAsync().ConfigureAwait(false);
            if (sessionStatus.IsValid)
            {
                // If yes, go straight to MainPageModel
                await _navigationService.NavigateToAsync<MainPageModel>(setRoot: true);
                return;
            }
            // else, stop loading and show Login/Signup
            IsLoading = false;
        }

        private async Task<bool> TryLoginWithCredentialsAsync()
        {
            var loginResult = await _accountRepository.LoginAsync(UsernameViewModel.Text, PasswordViewModel.Text).ConfigureAwait(false);

            return loginResult.IsSuccessful;
        }
    }
}