using InStock.Frontend.Abstraction.PageModels;
using InStock.Frontend.Abstraction.Services.Navigation;

namespace InStock.Frontend.Mobile.Services.Navigation
{
    public class MauiNavigationService : INavigationService
    {
        private readonly ILocator<Page> _locator;

        public MauiNavigationService(ILocator<Page> locator)
        {
            _locator = locator;
        }

        public Task NavigateToAsync<TPageModel>(object navigationData = null, bool setRoot = false)
            where TPageModel : class, IBasePageModel
        {
            if (Application.Current.Dispatcher.IsDispatchRequired)
            {
                return Application.Current.Dispatcher.DispatchAsync(() => NavigateToAsync<TPageModel>(navigationData, setRoot));
            }

            var page = _locator.CreatePageFor<TPageModel>();
            var tasks = new List<Task> { ((IBasePageModel)page.BindingContext).InitializeAsync(navigationData) };

            if (!setRoot)
            {
                tasks.Add(Application.Current.MainPage.Navigation.PushAsync(page));
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }

            return Task.WhenAll(tasks);
        }

        public Task PopAsync()
        {
            if (Application.Current.Dispatcher.IsDispatchRequired)
            {
                return Application.Current.Dispatcher.DispatchAsync(PopAsync);
            }
            return Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

