using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
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

        public MainPageModel(
            INavigationService navigationService)
		{
            _navigationService = navigationService;

            HeaderViewModel = new MainPageHeaderViewModel()
            {
                Title = Strings.PageTitle_MainPage
            };

            Items = new System.Collections.ObjectModel.ObservableCollection<BaseViewModel>
            {
                new SearchBarViewModel(),
                new ChartViewModel(),
                new CollectionViewModel<LocationCardViewModel>()
            };
        }
    }
}