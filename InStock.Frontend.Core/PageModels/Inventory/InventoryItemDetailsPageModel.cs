using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Input;

namespace InStock.Frontend.Core.PageModels.Inventory
{
	public class InventoryItemDetailsPageModel : BaseItemDetailsPageModel
	{
        public InventoryItemDetailsPageModel()
        {
            ConfirmViewModel = new ButtonViewModel
            {
                Title = Strings.Update
            };

            CancelViewModel = new ButtonViewModel
            {
                Title = Strings.Delete
            };
        }

        public override Task InitializeAsync(object? navigationData = null)
        {
            if (navigationData is InventoryItem item)
            {
                Name = item.Name;
                Description = item.Description;
            }

            return base.InitializeAsync(navigationData);
        }
    }
}

