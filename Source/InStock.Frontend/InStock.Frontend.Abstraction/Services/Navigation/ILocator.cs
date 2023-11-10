using InStock.Frontend.Abstraction.PageModels;

namespace InStock.Frontend.Abstraction.Services.Navigation
{
	public interface ILocator<TBasePageType>
	{
		TBasePageType CreatePageFor<TPageModel>()
			where TPageModel : class, IBasePageModel;

		void RegisterPageAndPageModel<TPage, TPageModel>()
			where TPage : class, TBasePageType
			where TPageModel : class, IBasePageModel;
	}
}