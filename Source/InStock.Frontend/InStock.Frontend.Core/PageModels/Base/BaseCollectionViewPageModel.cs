using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Validations;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BaseCollectionViewPageModel : BasePageModel
	{
		[ObservableProperty]
		private IList<INotifyPropertyChanged>? _items;

		[ObservableProperty]
		private bool _navigationBarVisible = false;

		[ObservableProperty]
		private IList<INotifyPropertyChanged>? _headerViewModels;

        [ObservableProperty]
        private IList<INotifyPropertyChanged>? _footerViewModels;

		protected virtual bool Validate()
		{
			var invalidItems = Items?.OfType<IValidatable>().Where(x => !x.Validate()).ToList();
			return invalidItems == null || invalidItems.Count == 0;
		}

		[RelayCommand]
		private async Task OnCompleteAsync()
		{
			if (!Validate())
			{
                return;
            }

			await OnCompleteValidatedAsync()
				.ConfigureAwait(false);
		}

		protected virtual Task OnCompleteValidatedAsync()
		{
			return Task.CompletedTask;
		}
    }
}
