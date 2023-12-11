using InStock.Common.IoC;
using InStock.Frontend.Mobile.Pages.Login;
using InStock.Frontend.Mobile.Pages.PointOfSale;
using InStock.Frontend.Mobile.Services.Threading;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.Login;
using InStock.Frontend.Core.PageModels.PointOfSale;
using InStock.Frontend.Core.Repositories;
using InStock.Frontend.Core.Services.Threading;
using InStock.Frontend.Mobile.Pages.Inventory;
using InStock.Frontend.Mobile.Pages.Shared;
using InStock.Frontend.Mobile.Services.Alerts;
using InStock.Frontend.Mobile.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Core.Services.Platform;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Frontend.Core.Managers;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Services.Settings;
using InStock.Frontend.Mobile.Services.Settings;
using InStock.Frontend.Mobile.Services.Platforms;
using InStock.Frontend.Mobile.Services.Platform;

namespace InStock.Frontend.Mobile;

public partial class App : Application
{
    public App()
    {
        RegisterServices();

        RegisterForNavigation();

        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var locator = Resolver.Resolve<ILocator<Page>>();
        var page = locator.CreatePageFor<MainPageModel>();
        var window = new Window(new NavigationPage(page));

        return window;
    }

    private static void RegisterServices()
    {
        var container = Resolver.Container;

        container.Register<ILocator<Page>>(new PageModelLocator(container));
        container.Register<IAlertService, MauiAlertService>();
        container.Register<IImageService, ImageService>();
        container.Register<INavigationService, MauiNavigationService>();
        container.Register<IMainThreadDispatcher, MainThreadDispatcher>();
        container.Register<ITaskCancellationService, TaskCancellationService>();
        container.Register<IClientInfoService, ClientInfoService>();
        container.Register<IPlatformInfoService, PlatformInfoService>();

        var apiRegistrar = new API.APIServiceRegistrar();
        container.Register(apiRegistrar.GetService<IAccountService>(new HttpClient()
        {
            BaseAddress = new Uri(Common.AccountService.Abstraction.Constants.BaseUrl),
            Timeout = TimeSpan.FromSeconds(10)
        }));

        container.Register(apiRegistrar.GetService<IInventoryService>(new HttpClient()
        {
            BaseAddress = new Uri(Common.InventoryService.Abstraction.Constants.BaseUrl),
            Timeout = TimeSpan.FromSeconds(10)
        }));

        container.Register<IAccountManager, AccountManager>();
        container.Register<ISettingsService, SettingsService>();
        container.Register<ISettingsManager, SettingsManager>();
        container.Register<ISessionManager, SessionManager>();
        container.Register<IInventoryRepository, InventoryRepository>();
    }

    private static void RegisterForNavigation()
    {
        var locator = Resolver.Resolve<ILocator<Page>>();

        //-- Dashboard
        locator.RegisterPageAndPageModel<CollectionViewPage, MainPageModel>();

        //-- Inventory
        locator.RegisterPageAndPageModel<ItemDetailsPage, InventoryItemDetailsPageModel>();
        locator.RegisterPageAndPageModel<CollectionViewPage, InventoryPageModel>();
        
        //-- Login
        locator.RegisterPageAndPageModel<CreateAccountPage, CreateAccountPageModel>();
        locator.RegisterPageAndPageModel<LoginPage, LoginPageModel>();

        //-- Scanner
        locator.RegisterPageAndPageModel<ScannerPage, ScannerPageModel>();
    }
}