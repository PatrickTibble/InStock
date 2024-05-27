using InStock.Frontend.Abstraction.PageModels;
using InStock.Frontend.Abstraction.Services.Navigation;

namespace InStock.Frontend.Mobile.Services.Navigation
{
    public class MauiNavigationService : INavigationService
    {
        private readonly ILocator<Page> _locator;

        private INavigation _navigation => Application.Current.MainPage.Navigation;
        private IDispatcher _dispatcher => Application.Current.Dispatcher;

        public MauiNavigationService(ILocator<Page> locator)
        {
            _locator = locator;
        }

        public async Task NavigateToAsync<TPageModel>(object? navigationData = null, bool setRoot = false)
            where TPageModel : class, IBasePageModel
        {
            var page = _locator.CreatePageFor<TPageModel>();

            if (_dispatcher.IsDispatchRequired)
            {
                await _dispatcher
                    .DispatchAsync(() => InternalNavigateAsync(page, setRoot))
                    .ConfigureAwait(false);
            }
            else
            {
                await InternalNavigateAsync(page, setRoot)
                    .ConfigureAwait(false);
            }

            if (page.BindingContext is IBasePageModel basePageModel)
            {
                await basePageModel
                    .InitializeAsync(navigationData)
                    .ConfigureAwait(false);
            }
        }

        private async Task InternalNavigateAsync(Page page, bool setRoot)
        {
            if (!setRoot)
            {
                await _navigation
                    .PushAsync(page)
                    .ConfigureAwait(false);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
        }

        public async Task PopAsync()
        {
            if (_dispatcher.IsDispatchRequired)
            {
                await _dispatcher
                    .DispatchAsync(PopAsync)
                    .ConfigureAwait(false);
                return;
            }
            await _navigation
                .PopAsync()
                .ConfigureAwait(false);
        }
    }
}

