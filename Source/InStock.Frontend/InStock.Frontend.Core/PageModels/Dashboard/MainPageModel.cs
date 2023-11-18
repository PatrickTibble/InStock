using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.PointOfSale;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Headers;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Frontend.Core.PageModels.Dashboard
{
	public class MainPageModel : BaseCollectionViewPageModel<MenuItemViewModel>
	{
        private readonly INavigationService _navigationService;

        public MainPageModel(
            INavigationService navigationService)
		{
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

        private Task OnShowInventory()
            => _navigationService.NavigateToAsync<InventoryPageModel>();

        private Task OnShowPointOfSale()
            => _navigationService.NavigateToAsync<ScannerPageModel>();
    }
}