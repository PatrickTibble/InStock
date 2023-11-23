using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Headers;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Frontend.Core.PageModels.Inventory
{
    public class InventoryPageModel : BaseCollectionViewPageModel<MenuItemViewModel>
    {
        private readonly IMainThreadDispatcher _dispatcher;
        private readonly INavigationService _navigationService;
        private readonly IInventoryRepository _repository;

        public InventoryPageModel(
            INavigationService navigationService,
            IInventoryRepository repository,
            IMainThreadDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _navigationService = navigationService;
            _repository = repository;

            NavigationBarVisible = true;

            HeaderViewModel = new PrimaryHeaderViewModel
            {
                Title = Strings.PageTitle_InventoryPage
            };
        }

        public override async Task InitializeAsync(object? navigationData = null)
        {
            var items = await _repository.GetFullInventoryAsync().ConfigureAwait(false);
            await Task.WhenAny(
                base.InitializeAsync(navigationData),
                _dispatcher.DispatchOnMainThreadAsync(() =>
                {
                    if (items != null)
                    {
                        Items = new ObservableCollection<MenuItemViewModel>(
                            items.Select(
                                item => new MenuItemViewModel(
                                    item.Name,
                                    item.Description,
                                    new RelayCommand(
                                        () => _navigationService.NavigateToAsync<InventoryItemDetailsPageModel>(item)
                                        ))));
                    }
                })).ConfigureAwait(false);
        }
    }
}