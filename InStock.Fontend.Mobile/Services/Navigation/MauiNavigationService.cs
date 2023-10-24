using InStock.Fontend.Mobile;
using InStock.Frontend.Abstraction.PageModels;
using InStock.Frontend.Abstraction.Services.Navigation;

namespace InStock.Frontend.Mobile.Services.Navigation
{
    public class MauiNavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            var root = new ContentPage();

            App.Current.MainPage = new NavigationPage(root);

            return Task.CompletedTask;
        }

        public Task NavigateToAsync<TPageModel>(object? navigationData = null)
            where TPageModel : IBasePageModel
        {
            return Task.CompletedTask;
        }

        public Task PopAsync()
        {
            return Task.CompletedTask;
        }
    }
}

