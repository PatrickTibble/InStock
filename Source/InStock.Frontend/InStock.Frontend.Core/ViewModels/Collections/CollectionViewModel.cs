using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;
using System.Collections.ObjectModel;

namespace InStock.Frontend.Core.ViewModels.Collections
{
    public partial class CollectionViewModel<T> : BaseViewModel
        where T : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<T>? _items;
    }
}
