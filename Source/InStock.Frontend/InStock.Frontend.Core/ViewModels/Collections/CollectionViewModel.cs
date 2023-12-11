using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;
using System.Collections.ObjectModel;

namespace InStock.Frontend.Core.ViewModels.Collections
{
    public partial class CollectionViewModel<T> : BaseViewModel
        where T : BaseViewModel
    {
        private readonly Func<Task<IEnumerable<T>>> _retrievalTask;

        [ObservableProperty]
        private ObservableCollection<T>? _items;

        public CollectionViewModel(Func<Task<IEnumerable<T>>> retrievalTask)
        {
            _retrievalTask = retrievalTask;
        }

        public async Task InitializeAsync(object? navigationData = null)
        {
            var items = await _retrievalTask();
            Items = new ObservableCollection<T>(items);
        }
    }
}
