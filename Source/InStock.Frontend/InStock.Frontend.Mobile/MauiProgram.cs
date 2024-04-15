using CommunityToolkit.Maui;
using InStock.Common.IoC;
using InStock.Frontend.Mobile.Extensions;
using InStock.Frontend.Mobile.Services;
using Microsoft.Extensions.Logging;

namespace InStock.Frontend.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Resolver.SetServiceHelper(new ServiceHelper());
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiCommunityToolkit()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Logging.AddDebug();
#endif

		builder.Services
			.RegisterServices()
			.RegisterForNavigation();

		return builder.Build();
	}
}