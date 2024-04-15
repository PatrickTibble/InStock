using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.IoC;
using InStock.Frontend.Abstraction.Services.Navigation;

namespace InStock.Frontend.Core.Extensions.Navigation
{
	public static class INavigationServiceExtensions
	{

		public static Task NavigateToInventoryAsync(this INavigationService navigationService)
		{
			Resolver.Resolve<ILogger>().LogInfo("Navigate to Inventory Page");
			return Task.CompletedTask;
		}

		public static Task NavigateToPointOfSaleAsync(this INavigationService navigationService)
		{
            Resolver.Resolve<ILogger>().LogInfo("Navigate to Point of Sale Page");
			return Task.CompletedTask;
		}
	}
}

