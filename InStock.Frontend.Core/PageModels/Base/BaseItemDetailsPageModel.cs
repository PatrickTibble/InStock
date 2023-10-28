using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Input;

namespace InStock.Frontend.Core.PageModels.Base
{
	public abstract partial class BaseItemDetailsPageModel : BasePageModel
	{
		[ObservableProperty]
		private string? _imageUrl;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private ButtonViewModel? _confirmViewModel;

        [ObservableProperty]
        private ButtonViewModel? _cancelViewModel;
	}
}

