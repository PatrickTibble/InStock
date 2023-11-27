using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.ViewModels.Cards
{
    public partial class EntryCardViewModel : ValidatableViewModel
    {
        [ObservableProperty]
        private string? _placeholder;

        [ObservableProperty]
        private bool _isVisible = true;

        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isPassword;

        [ObservableProperty]
        private string? _leftIcon;

        [ObservableProperty]
        private string? _rightIcon;
    }
}