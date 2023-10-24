using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.Extensions.Navigation;
using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;

namespace InStock.Frontend.Core.PageModels.Dashboard
{
	public class MainPageModel : BaseCollectionViewPageModel<MenuItem>
	{
        private readonly INavigationService navigationService;

        public MainPageModel(INavigationService navigationService)
		{
            this.navigationService = navigationService;

            MenuItems.Add(
                new MenuItem(
                    Strings.MenuItem_Title_Inventory,
                    Strings.MenuItem_Subtitle_Inventory,
                    new AsyncRelayCommand(OnShowInventory)));

            MenuItems.Add(
                new MenuItem(
                    Strings.MenuItem_Title_PointOfSale,
                    Strings.MenuItem_Subtitle_PointOfSale,
                    new AsyncRelayCommand(OnShowPointOfSale)));
        }

        private Task OnShowPointOfSale()
            => navigationService.NavigateToPointOfSaleAsync();

        private Task OnShowInventory()
            => navigationService.NavigateToInventoryAsync();
    }
}