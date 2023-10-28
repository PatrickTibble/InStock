using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Dashboard;

namespace InStock.Frontend.Core.PageModels.Login
{
    public class LoginPageModel : BasePageModel
	{
        private readonly INavigationService _navigationService;

        public LoginPageModel(INavigationService navigationService)
		{
            _navigationService = navigationService;
		}

        public override async Task InitializeAsync(object? navigationData = null)
        {
            await base.InitializeAsync(navigationData);
            // Determine if user is logged in and if current
            // session is active.

            // If yes, go straight to MainPage
            await _navigationService.NavigateToAsync<MainPageModel>(setRoot: true);

            // else, stop loading and show Login/Signup
        }
    }
}

