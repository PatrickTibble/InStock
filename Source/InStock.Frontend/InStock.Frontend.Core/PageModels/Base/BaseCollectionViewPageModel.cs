using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BaseCollectionViewPageModel : BasePageModel
	{
		[ObservableProperty]
		private ObservableCollection<BaseViewModel>? _items;

		[ObservableProperty]
		private bool _navigationBarVisible = false;

		[ObservableProperty]
		private BaseViewModel? _selectedItem;

		public ObservableCollection<BaseViewModel>? HeaderViewModels { get; }

		public BaseViewModel? HeaderViewModel
		{
			get => HeaderViewModels?.FirstOrDefault();
			set
			{
				HeaderViewModels?.Clear();
				if (value != null)
				{
					HeaderViewModels?.Add(value);
				}
			}
		}

		protected BaseCollectionViewPageModel()
		{
            HeaderViewModels = new ObservableCollection<BaseViewModel>();
        }

		protected virtual void SelectedItemChanged(BaseViewModel? oldValue, BaseViewModel? newValue)
		{

		}

		partial void OnSelectedItemChanged(BaseViewModel? oldValue, BaseViewModel? newValue)
			=> SelectedItemChanged(oldValue, newValue);
    }
}

