using InStock.Common.Abstraction.Services.Downloading;
using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.Abstraction.Services.Storage;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.Core.Services.Downloading;
using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Frontend.Abstraction.Adapters;
using InStock.Frontend.Abstraction.Directors;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.PageModels;
using InStock.Frontend.Abstraction.Repositories;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Abstraction.Services.Threading;
using InStock.Frontend.Core.Adapters;
using InStock.Frontend.Core.Builders;
using InStock.Frontend.Core.Directors;
using InStock.Frontend.Core.Factories;
using InStock.Frontend.Core.Managers;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Inventory;
using InStock.Frontend.Core.PageModels.Login;
using InStock.Frontend.Core.PageModels.PointOfSale;
using InStock.Frontend.Core.Repositories;
using InStock.Frontend.Core.Services.Platform;
using InStock.Frontend.Core.Services.Threading;
using InStock.Frontend.Mobile.Pages.Inventory;
using InStock.Frontend.Mobile.Pages.Login;
using InStock.Frontend.Mobile.Pages.PointOfSale;
using InStock.Frontend.Mobile.Pages.Shared;
using InStock.Frontend.Mobile.Services.Alerts;
using InStock.Frontend.Mobile.Services.Logger;
using InStock.Frontend.Mobile.Services.Navigation;
using InStock.Frontend.Mobile.Services.Platform;
using InStock.Frontend.Mobile.Services.Platforms;
using InStock.Frontend.Mobile.Services.Threading;

namespace InStock.Frontend.Mobile.Extensions;

public static class IServiceCollectionExtensions
{

    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        //-- Service Registrations
        collection
            .AddSingleton<IDownloadingService, DownloadingService>()
            .AddSingleton<ILogger, DebugLogger>()
            .AddSingleton<IAlertService, MauiAlertService>()
            .AddSingleton<IImageService, ImageService>()
            .AddSingleton<INavigationService, MauiNavigationService>()
            .AddSingleton<IMainThreadDispatcher, MainThreadDispatcher>()
            .AddSingleton<ITaskCancellationService, TaskCancellationService>()
            .AddSingleton<IClientInfoService, ClientInfoService>()
            .AddSingleton<IPlatformInfoService, PlatformInfoService>();

        //-- API Manager Registrations
        collection
            .AddTransient<IAccountManager, AccountManager>()
            .AddTransient<IStorageService, LocalStorageService>()
            .AddTransient<IStorageManager, LocalStorageManager>()
            .AddTransient<ISessionManager, SessionManager>()
            .AddTransient<IInventoryRepository, InventoryRepository>()
            .AddTransient<ILocationsManager, LocationsManager>()
            .AddTransient<IRevenueManager, RevenueManager>();

        //-- API Registrations
        var inventoryServiceUrl = Common.InventoryService.Abstraction.Constants.BaseUrl;
        var accountServiceUrl = Common.AccountService.Abstraction.Constants.BaseUrl;
        var apiRegistrar = new API.APIServiceRegistrar();
        collection
            .AddSingleton(apiRegistrar.GetService<IAccountService>(CreateHttpClient(accountServiceUrl)))
            .AddSingleton(apiRegistrar.GetService<IInventoryService>(CreateHttpClient(inventoryServiceUrl)))
            .AddSingleton(apiRegistrar.GetService<IRevenueService>(CreateHttpClient(inventoryServiceUrl)))
            .AddSingleton(apiRegistrar.GetService<ILocationsService>(CreateHttpClient(inventoryServiceUrl)));

        //-- Adapters
        collection
            .AddSingleton<IAdapter<IList<RevenueReport>, ChartDataSet>, RevenueToChartDataSetAdapter>();

        //-- Directors
        var ruleFactory = new ValidationRuleFactory();
        var builder = new ViewModelBuilder();
        var director = new ViewModelDirector();
        director.SetBuilder(builder);
        director.SetRuleFactory(ruleFactory);

        collection
            .AddSingleton<IViewModelDirector>(director);

        return collection;
    }

    public static IServiceCollection RegisterForNavigation(this IServiceCollection collection)
    {
        var locator = new PageModelLocator();

        collection
            .AddSingleton<ILocator<Page>>(locator)
            //-- Dashboard
            .Register<MainPageModel>(locator)

            //-- Inventory
            .Register<ItemDetailsPage, InventoryItemDetailsPageModel>(locator)
            .Register<CollectionViewPage, InventoryPageModel>(locator)

            //-- Login
            .Register<CreateAccountPage, CreateAccountPageModel>(locator)
            .Register<LoginPageModel>(locator)

            //-- Scanner
            .Register<ScannerPage, ScannerPageModel>(locator);

        return collection;
    }

    private static IServiceCollection Register<TPageModel>(this IServiceCollection collection, ILocator<Page> locator)
        where TPageModel : class, IBasePageModel
        => Register<CollectionViewPage, TPageModel>(collection, locator);

    private static IServiceCollection Register<TPage, TPageModel>(this IServiceCollection collection, ILocator<Page> locator)
        where TPageModel : class, IBasePageModel
        where TPage : Page
    {
        collection.AddTransient<TPageModel>();
        locator.RegisterPageAndPageModel<TPage, TPageModel>();
        return collection;
    }

    private static HttpClient CreateHttpClient(string baseUrl)
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromSeconds(10)
        };
    }
}
