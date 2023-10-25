using InStock.Common.IoC;
using InStock.Frontend.Mobile.Pages.Dashboard;
using InStock.Frontend.Mobile.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.Extensions;
using InStock.Frontend.Core.Models;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.Repositories.Mocks;
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
	}

    protected override void OnStart()
    {
        base.OnStart();
        var navigationService = Resolver.Resolve<INavigationService>();
        navigationService
            .NavigateToAsync<MainPageModel>(setRoot: true)
            .FireAndForgetSafeAsync();
    }

    private static void RegisterServices()
    {
        var container = Resolver.Container;

        container.Register<ILocator<Page>>(new PageModelLocator(container));
        container.Register<IAlertService, MauiAlertService>();
        container.Register<INavigationService, MauiNavigationService>();

        //-- TODO: Add configuration for DebugWithMocks
        container.Register<IRepository<InventoryItem>, MockInventoryRepository>();
    }

    private static void RegisterForNavigation()
    {
        var locator = Resolver.Resolve<ILocator<Page>>();
        locator.RegisterPageAndPageModel<MainPage, MainPageModel>();
        locator.RegisterPageAndPageModel<CollectionViewPage, InventoryPageModel>();
        locator.RegisterPageAndPageModel<ItemDetailsPage, InventoryItemDetailsPageModel>();
    }
}