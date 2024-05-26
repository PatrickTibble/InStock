using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Frontend.Abstraction.Adapters;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.Services.Platform;
using InStock.Frontend.Core.ViewModels.Cards;
using InStock.Frontend.Core.ViewModels.Collections;
using InStock.Frontend.Core.ViewModels.Headers;
using InStock.Frontend.Core.ViewModels.Input;
using System.ComponentModel;

namespace InStock.Frontend.Core.PageModels.Dashboard
{
    public class MainPageModel : BaseCollectionViewPageModel
    {
        private readonly INavigationService _navigationService;
        private readonly IImageService _imageService;
        private readonly IAdapter<IList<RevenueReport>, ChartDataSet> _revenueAdapter;
        private readonly ILocationsManager _locationsManager;
        private readonly IRevenueManager _revenueManager;
        private ChartViewModel _chartViewModel;
        private CollectionViewModel<LocationCardViewModel> _locationsViewModel;

        public MainPageModel(
            INavigationService navigationService,
            IImageService imageService,
            ILocationsManager locationManager,
            IRevenueManager revenueManager,
            IAdapter<IList<RevenueReport>, ChartDataSet> revenueAdapter)
        {
            _locationsManager = locationManager;
            _revenueManager = revenueManager;
            _navigationService = navigationService;
            _imageService = imageService;
            _revenueAdapter = revenueAdapter;

            HeaderViewModels = new List<INotifyPropertyChanged> 
            {
                new MainPageHeaderViewModel()
                {
                    Title = Strings.PageTitle_MainPage
                }
            };

            _chartViewModel = new ChartViewModel();
            _locationsViewModel = new CollectionViewModel<LocationCardViewModel>(GetLocationsAsync);

            Items = new System.Collections.ObjectModel.ObservableCollection<INotifyPropertyChanged>
            {
                new SearchBarViewModel
                {
                    LeftIcon = _imageService.GetImage(Abstraction.Enums.Images.Search),
                    RightIcon = _imageService.GetImage(Abstraction.Enums.Images.Chevron),
                    Placeholder = Strings.Placeholder_Search
                },
                _chartViewModel,
                _locationsViewModel
            };
        }

        public override async Task InitializeAsync(object? navigationData = null)
        {
            var revenue = await _revenueManager
                .GetRevenueReportAsync()
                .ConfigureAwait(false);

            var chartData = _revenueAdapter.Convert(revenue ?? new List<RevenueReport>());

            await Task.WhenAny(
                    base.InitializeAsync(navigationData),
                    _chartViewModel.InitializeAsync(chartData),
                    _locationsViewModel.InitializeAsync())
                .ConfigureAwait(false);
        }

        private async Task<IEnumerable<LocationCardViewModel>> GetLocationsAsync()
        {
            var locationsResult = await _locationsManager
                .GetLocationsAsync()
                .ConfigureAwait(false);

            return locationsResult.Select(x => new LocationCardViewModel(x));
        }
    }
}