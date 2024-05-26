using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Core.ViewModels.Base;

public abstract partial class BaseVisualElementViewModel : BaseViewModel
{
    [ObservableProperty]
    private MarginSet _margins = MarginSet.Default;

    [ObservableProperty]
    private MarginSet _padding = MarginSet.Default;

    [ObservableProperty]
    private bool _isVisible = true;

    [ObservableProperty]
    private bool _isEnabled = true;
}
