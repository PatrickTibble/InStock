using CommunityToolkit.Mvvm.Input;
using InStock.Common.Core.Extensions;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.Login;
using InStock.Frontend.Core.PageModels.PointOfSale;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Headers;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Frontend.Core.PageModels.Dashboard
{
	public class MainPageModel : BaseCollectionViewPageModel<MenuItemViewModel>
	{
        private readonly ISessionManager _sessionManager;
        private readonly INavigationService _navigationService;

        public MainPageModel(
            INavigationService navigationService,
            ISessionManager sessionManager)
		{
            _sessionManager = sessionManager;
            _navigationService = navigationService;

            HeaderViewModel = new MainPageHeaderViewModel()
            {
                Title = Strings.PageTitle_MainPage
            };

            Items = new System.Collections.ObjectModel.ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel(
                    Strings.MenuItem_Title_Inventory,
                    Strings.MenuItem_Subtitle_Inventory,
                    new AsyncRelayCommand(OnShowInventory)),

                new MenuItemViewModel(
                    Strings.MenuItem_Title_PointOfSale,
                    Strings.MenuItem_Subtitle_PointOfSale,
                    new AsyncRelayCommand(OnShowPointOfSale))
            };
        }

        public override void Appearing(object? sender, EventArgs e)
        {
            base.Appearing(sender, e);
            VerifyUserSessionAsync().FireAndForgetSafeAsync();
        }

        private async Task VerifyUserSessionAsync()
        {
            var sessionStatus = await _sessionManager.ValidateSessionAsync().ConfigureAwait(false);
            if (!sessionStatus)
            {
                await _navigationService.NavigateToAsync<LoginPageModel>().ConfigureAwait(false);
            }
        }

        private Task OnShowInventory()
            => _navigationService.NavigateToAsync<InventoryPageModel>();

        private Task OnShowPointOfSale()
            => _navigationService.NavigateToAsync<ScannerPageModel>();
    }
}