using CommunityToolkit.Mvvm.ComponentModel;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BasePageModel : ObservableObject
	{
		[ObservableProperty]
		protected string? title;

		public virtual Task InitializeAsync()
		{
			return Task.CompletedTask;
		}
	}
}