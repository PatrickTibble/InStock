using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.PointOfSale;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Frontend.Core.PageModels.Dashboard
{
	public class MainPageModel : BaseCollectionViewPageModel<MenuItemViewModel>
	{
        private readonly INavigationService navigationService;

        public MainPageModel(INavigationService navigationService)
		{
            this.navigationService = navigationService;
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
            => navigationService.NavigateToAsync<InventoryPageModel>();

        private Task OnShowPointOfSale()
            => navigationService.NavigateToAsync<ScannerPageModel>();
    }
}