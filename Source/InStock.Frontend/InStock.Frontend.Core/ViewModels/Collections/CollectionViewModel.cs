using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;
using System.Collections.ObjectModel;

namespace InStock.Frontend.Core.ViewModels.Collections
{
    public partial class CollectionViewModel : BaseViewModel
    {
        private ObservableCollection<BaseViewModel>? _items;
        public ObservableCollection<BaseViewModel>? Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
    }

    public partial class CollectionViewModel<TViewModel> : CollectionViewModel
        where TViewModel : BaseViewModel
    {
        private readonly Func<Task<IEnumerable<TViewModel>>> _retrievalTask;

        private ObservableCollection<TViewModel>? _items;

        public new ObservableCollection<TViewModel>? Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public CollectionViewModel(Func<Task<IEnumerable<TViewModel>>> retrievalTask)
        {
            _retrievalTask = retrievalTask;
        }

        public async override Task InitializeAsync(object? navigationData = null)
        {
            var items = await _retrievalTask();
            Items = new ObservableCollection<TViewModel>(items);
        }
    }
}
