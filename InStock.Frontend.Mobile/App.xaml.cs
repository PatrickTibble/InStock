using InStock.Common.Abstraction.Repositories.Base;
using InStock.Common.IoC;
using InStock.Fontend.Mobile.Pages.PointOfSale;
using InStock.Fontend.Mobile.Services.Threading;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.API.Account;
using InStock.Frontend.API.Inventory;
using InStock.Frontend.Core.Extensions;
using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.Login;
using InStock.Frontend.Core.PageModels.PointOfSale;
using InStock.Frontend.Core.Repositories.Mocks;
using InStock.Frontend.Mobile.Pages.Inventory;
using InStock.Frontend.Mobile.Pages.Shared;
using InStock.Frontend.Mobile.Services.Alerts;
using InStock.Frontend.Mobile.Services.Navigation;

namespace InStock.Frontend.Mobile;

public partial class App : Application
{
	public App()
	{
        RegisterServices();

        RegisterForNavigation();

		InitializeComponent();

        InitializeNavigation();
    }

    private static void InitializeNavigation()
    {
        var navigationService = Resolver.Resolve<INavigationService>();
        navigationService
            .NavigateToAsync<LoginPageModel>(setRoot: true)
            .FireAndForgetSafeAsync();
    }

    private static void RegisterServices()
    {
        var container = Resolver.Container;

        container.Register<ILocator<Page>>(new PageModelLocator(container));
        container.Register<IAlertService, MauiAlertService>();
        container.Register<INavigationService, MauiNavigationService>();
        container.Register<IMainThreadDispatcher, MainThreadDispatcher>();

        //-- TODO: Add configuration for DebugWithMocks
        container.Register<IRepository<InventoryItem>, MockInventoryRepository>();

        var httpClient = new HttpClient();
        var apiRegistrar = new API.APIServiceRegistrar();
        container.Register(apiRegistrar.GetService<IAccountService>(httpClient));
        container.Register(apiRegistrar.GetService<IInventoryService>(httpClient));
    }

    private static void RegisterForNavigation()
    {
        var locator = Resolver.Resolve<ILocator<Page>>();
        locator.RegisterPageAndPageModel<CollectionViewPage, MainPageModel>();
        locator.RegisterPageAndPageModel<CollectionViewPage, InventoryPageModel>();
        locator.RegisterPageAndPageModel<ScannerPage, ScannerPageModel>();
        locator.RegisterPageAndPageModel<ItemDetailsPage, InventoryItemDetailsPageModel>();
    }
}