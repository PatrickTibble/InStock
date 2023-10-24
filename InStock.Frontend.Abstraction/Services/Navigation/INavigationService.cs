using InStock.Frontend.Abstraction.PageModels;

namespace InStock.Frontend.Abstraction.Services.Navigation
{
	public interface INavigationService
	{
        Task InitializeAsync();

        Task NavigateToAsync<TPageModel>(object? navigationData = null)
            where TPageModel : IBasePageModel;

        Task PopAsync();
    }
}