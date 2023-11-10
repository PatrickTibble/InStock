using InStock.Frontend.Abstraction.PageModels;

namespace InStock.Frontend.Abstraction.Services.Navigation
{
	public interface INavigationService
	{
        Task NavigateToAsync<TPageModel>(object? navigationData = null, bool setRoot = false)
            where TPageModel : class, IBasePageModel;

        Task PopAsync();
    }
}