using InStock.Common.IoC;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Common.Core.Extensions;
using InStock.Frontend.Core.PageModels.Login;

namespace InStock.Frontend.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var locator = Resolver.Resolve<ILocator<Page>>();
        var page = locator.CreatePageFor<LoginPageModel>();

        MainPage = new NavigationPage(page);
        if (page.BindingContext is BasePageModel pm)
        {
            pm.InitializeAsync().FireAndForgetSafeAsync();
        }
    }
}
