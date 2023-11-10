using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.ViewModels.Input
{
	public partial class ButtonViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isVisible = true;

        [ObservableProperty]
        private ICommand? _command;
	}
}

