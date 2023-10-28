using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Common.Abstraction.Repositories.Base;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Frontend.Core.PageModels.Inventory
{
	public class InventoryPageModel : BaseCollectionViewPageModel<MenuItemViewModel>
	{
        private readonly IDispatcher _dispatcher;
        private readonly INavigationService _navigationService;
        private readonly IRepository<InventoryItem> _repository;

        public InventoryPageModel(
            INavigationService navigationService,
            IRepository<InventoryItem> repository,
            IDispatcher dispatcher)
		{
            _dispatcher = dispatcher;
            _navigationService = navigationService;
            _repository = repository;
		}

        public override async Task InitializeAsync(object? navigationData = null)
        {
            var items = _repository.GetAll();
            await _dispatcher.DispatchOnMainThreadAsync(() =>
            {
                Items = new ObservableCollection<MenuItemViewModel>(
                    items.Select(
                        item => new MenuItemViewModel(
                            item.Name,
                            item.Description,
                            new RelayCommand(() => _navigationService.NavigateToAsync<InventoryItemDetailsPageModel>(item)))));
            });
            await base.InitializeAsync(navigationData);
        }
    }
}