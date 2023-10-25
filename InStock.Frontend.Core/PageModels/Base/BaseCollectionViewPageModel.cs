using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BaseCollectionViewPageModel<TListItem> : BasePageModel
	{
		public ObservableCollection<TListItem> Items { get; }

		[ObservableProperty]
		private TListItem? _selectedItem;

		protected BaseCollectionViewPageModel()
		{
			Items = new ObservableCollection<TListItem>();
		}

		protected virtual void SelectedItemChanged(TListItem? oldValue, TListItem? newValue)
		{
			/* no operation here */
		}

        partial void OnSelectedItemChanged(TListItem? oldValue, TListItem? newValue)
			=> SelectedItemChanged(oldValue, newValue);
    }
}

