using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Dashboard;

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
        private string? _username;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private ICommand? _loginWithCredentialsCommand;

        [ObservableProperty]
        private ICommand? _loginWithTokenCommand;

        public LoginPageModel(
            INavigationService navigationService,
            ISessionRepository sessionRepository,
            IAccountRepository accountRepository)
		{
            _navigationService = navigationService;
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;

            LoginWithCredentialsCommand = new AsyncRelayCommand(TryLoginWithCredentialsAsync, () => !IsLoading);
            LoginWithTokenCommand = new AsyncRelayCommand(TryLoginWithTokenAsync, () => !IsLoading);
        }

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
            var loginResult = await _accountRepository.LoginAsync(Username, Password).ConfigureAwait(false);

            return loginResult.IsSuccessful;
        }

        private async Task<bool> TryLoginWithTokenAsync()
        {
            // get session token from biometrics
            return await Task.FromResult(false).ConfigureAwait(false);
        }
    }
}