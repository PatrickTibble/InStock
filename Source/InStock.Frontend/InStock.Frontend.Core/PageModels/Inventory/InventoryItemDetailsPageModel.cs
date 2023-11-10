using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Models;
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
                Title = Strings.Update,
                Command = new RelayCommand(OnConfirm)
            };

            CancelViewModel = new ButtonViewModel
            {
                Title = Strings.Delete,
                Command = new RelayCommand(OnCancel)
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

        private void OnCancel()
        {
            throw new NotImplementedException();
        }

        private void OnConfirm()
        {
            throw new NotImplementedException();
        }
    }
}