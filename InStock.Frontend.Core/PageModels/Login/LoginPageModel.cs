using CommunityToolkit.Mvvm.ComponentModel;
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

        [ObservableProperty]
        private bool _isLoading;

        public LoginPageModel(
            INavigationService navigationService,
            ISessionRepository sessionRepository)
		{
            _navigationService = navigationService;
            _sessionRepository = sessionRepository;
        }

        public override async Task InitializeAsync(object? navigationData = null)
        {
            IsLoading = true;
            await base.InitializeAsync(navigationData);
            // Determine if user is logged in and if current
            // session is active.
            var sessionStatus = await _sessionRepository.GetSessionStateAsync();
            if (sessionStatus.IsValid)
            {
                // If yes, go straight to MainPage
                await _navigationService.NavigateToAsync<MainPageModel>(setRoot: true);
                return;
            }
            // else, stop loading and show Login/Signup
            IsLoading = false;
        }
    }
}

