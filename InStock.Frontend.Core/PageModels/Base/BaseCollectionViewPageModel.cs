using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BaseCollectionViewPageModel<TListItem> : BasePageModel
	{
		public ObservableCollection<TListItem> MenuItems { get; }

		[ObservableProperty]
		private TListItem? _selectedItem;

		protected BaseCollectionViewPageModel()
		{
			MenuItems = new ObservableCollection<TListItem>();
		}
	}
}

