using InStock.Common.Abstraction.Repositories.Base;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.Extensions;
using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.PageModels.Base;

namespace InStock.Frontend.Core.PageModels.Inventory
{
	public class InventoryPageModel : BaseCollectionViewPageModel<InventoryItem>
	{
        private readonly INavigationService _navigationService;
        private readonly IRepository<InventoryItem> _repository;

        public InventoryPageModel(
            INavigationService navigationService,
            IRepository<InventoryItem> repository)
		{
            _navigationService = navigationService;
            _repository = repository;
		}

        public override Task InitializeAsync()
        {
            var items = _repository.GetAll();
            foreach (var item in items)
            {
                Items.Add(item);
            }

            return base.InitializeAsync();
        }

        protected override void SelectedItemChanged(InventoryItem? oldValue, InventoryItem? newValue)
        {
            base.SelectedItemChanged(oldValue, newValue);

            _navigationService
                .NavigateToAsync<InventoryItemDetailsPageModel>(newValue)
                .FireAndForgetSafeAsync();
        }
    }
}

