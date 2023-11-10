using InStock.Frontend.Abstraction.Services.Navigation;

namespace InStock.Frontend.Core.Extensions.Navigation
{
	public static class INavigationServiceExtensions
	{
		public static Task NavigateToInventoryAsync(this INavigationService navigationService)
		{
			System.Diagnostics.Debug.WriteLine("Navigate to Inventory Page");
			return Task.CompletedTask;
		}

		public static Task NavigateToPointOfSaleAsync(this INavigationService navigationService)
		{
			System.Diagnostics.Debug.WriteLine("Navigate to Point of Sale Page");
			return Task.CompletedTask;
		}
	}
}

