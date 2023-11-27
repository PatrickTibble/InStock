using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;
using System.Windows.Input;

namespace InStock.Frontend.Core.ViewModels.Cards
{
    public partial class LocationCardViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? _titleText;

        [ObservableProperty]
        private string? _detailText;

        [ObservableProperty]
        private string? _imageSource;

        [ObservableProperty]
        private ICommand? _command;
    }
}