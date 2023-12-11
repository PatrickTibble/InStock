using InStock.Frontend.Abstraction.Adapters;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.Services.Platform;
using InStock.Frontend.Core.ViewModels.Base;
using InStock.Frontend.Core.ViewModels.Cards;
using InStock.Frontend.Core.ViewModels.Collections;
using InStock.Frontend.Core.ViewModels.Headers;
using InStock.Frontend.Core.ViewModels.Input;

namespace InStock.Frontend.Core.PageModels.Dashboard
{
	public class MainPageModel : BaseCollectionViewPageModel<BaseViewModel>
	{
        private readonly INavigationService _navigationService;
        private readonly IImageService _imageService;
        private readonly IAdapter<RevenueReport, ChartDataSet> _revenueAdapter;
        private readonly ILocationsManager _locationsManager;
        private readonly IRevenueManager _revenueManager;
        private ChartViewModel _chartViewModel;
        private CollectionViewModel<LocationCardViewModel> _locationsViewModel;

        public MainPageModel(
            INavigationService navigationService,
            IImageService imageService,
            ILocationsManager locationManager,
            IRevenueManager revenueManager,
            IAdapter<RevenueReport, ChartDataSet> revenueAdapter)
		{
            _locationsManager = locationManager;
            _revenueManager = revenueManager;
            _navigationService = navigationService;
            _imageService = imageService;
            _revenueAdapter = revenueAdapter;

            HeaderViewModel = new MainPageHeaderViewModel()
            {
                Title = Strings.PageTitle_MainPage
            };

            _chartViewModel = new ChartViewModel();
            _locationsViewModel = new CollectionViewModel<LocationCardViewModel>(GetLocations);

            Items = new System.Collections.ObjectModel.ObservableCollection<BaseViewModel>
            {
                new SearchBarViewModel
                {
                    LeftIcon = _imageService.GetImage(Abstraction.Enums.Images.SearchIcon),
                    RightIcon = _imageService.GetImage(Abstraction.Enums.Images.ChevronIcon),
                    Placeholder = Strings.Placeholder_Search
                },
                _chartViewModel,
                _locationsViewModel
            };
        }

        public override async Task InitializeAsync(object? navigationData = null)
        {
            var revenue = await _revenueManager.GetRevenueReportAsync();
            await Task.WhenAny(
                base.InitializeAsync(navigationData),
                _chartViewModel.InitializeAsync(_revenueAdapter.Convert(revenue)),
                _locationsViewModel.InitializeAsync());
        }

        private async Task<IEnumerable<LocationCardViewModel>> GetLocations()
        {
            var locationsResult = await _locationsManager.GetLocationsAsync();
            return locationsResult.Select(x => new LocationCardViewModel(x));
        }
    }
}