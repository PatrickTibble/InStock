using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Mobile.Services.Alerts;
using InStock.Frontend.Mobile.Services.Navigation;
using Microsoft.Extensions.Logging;

namespace InStock.Fontend.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterServices()
			.RegisterForNavigation();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
	{
		builder.Services
			.AddSingleton<INavigationService, MauiNavigationService>()
			.AddSingleton<IAlertService, MauiAlertService>();
		return builder;
	}

	private static MauiAppBuilder RegisterForNavigation(this MauiAppBuilder builder)
	{
		builder.Services
			.AddTransient<MainPageModel>();
		return builder;
	}
}