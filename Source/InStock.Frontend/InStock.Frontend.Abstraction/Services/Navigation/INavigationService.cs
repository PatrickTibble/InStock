using InStock.Frontend.Abstraction.PageModels;

namespace InStock.Frontend.Abstraction.Services.Navigation
{
    /// <summary>
    /// Contract for the navigation service.
    /// </summary>
	public interface INavigationService
	{
        /// <summary>
        /// Perform a navigation to the specified page model.
        /// </summary>
        /// <typeparam name="TPageModel"></typeparam>
        /// <param name="navigationData"></param>
        /// <param name="setRoot"></param>
        /// <returns></returns>
        Task NavigateToAsync<TPageModel>(object? navigationData = null, bool setRoot = false)
            where TPageModel : class, IBasePageModel;

        /// <summary>
        /// Pop the current page from the navigation stack.
        /// </summary>
        /// <returns></returns>
        Task PopAsync();
    }
}