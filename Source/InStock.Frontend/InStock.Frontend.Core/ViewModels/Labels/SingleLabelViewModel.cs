using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.ViewModels.Labels;

public partial class SingleLabelViewModel : BaseVisualElementViewModel
{
    [ObservableProperty]
    private string _text = string.Empty;

    [ObservableProperty]
    private LabelStyle _labelStyle = LabelStyle.Default;
}
