using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Abstraction.PageModels;

namespace InStock.Frontend.Core.PageModels.Base
{
    public abstract partial class BasePageModel : ObservableObject, IBasePageModel
	{
		[ObservableProperty]
		protected string? title;

        public virtual void Appearing(object? sender, EventArgs e)
        {

        }

        public virtual void Disappearing(object? sender, EventArgs e)
        {

        }

        public virtual Task InitializeAsync()
		{
			return Task.CompletedTask;
		}
	}
}