using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.ViewModels.Input
{
    public partial class PrimaryEntryViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private string? _placeholder;

        [ObservableProperty]
        private bool _isVisible = true;

        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isPassword;
    }
}