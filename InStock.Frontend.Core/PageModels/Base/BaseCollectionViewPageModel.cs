using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BaseCollectionViewPageModel<TListItem> : BasePageModel
		where TListItem : BaseViewModel
	{
		[ObservableProperty]
		private ObservableCollection<TListItem>? _items;

		[ObservableProperty]
		private TListItem? _selectedItem;

		protected virtual void SelectedItemChanged(TListItem? oldValue, TListItem? newValue)
		{

		}

		partial void OnSelectedItemChanged(TListItem? oldValue, TListItem? newValue)
			=> SelectedItemChanged(oldValue, newValue);
    }
}

