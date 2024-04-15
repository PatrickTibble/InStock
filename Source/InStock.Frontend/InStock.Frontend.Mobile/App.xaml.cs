using InStock.Common.IoC;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Common.Core.Extensions;

namespace InStock.Frontend.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var locator = Resolver.Resolve<ILocator<Page>>();
        var page = locator.CreatePageFor<MainPageModel>();

        MainPage = page;
        if (page.BindingContext is BasePageModel pm)
        {
            pm.InitializeAsync().FireAndForgetSafeAsync();
        }
    }
}
