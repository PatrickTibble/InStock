using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.ViewModels.ListItems
{
    public partial class MenuItemViewModel : BaseViewModel
	{
        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private string? _subtitle;

        [ObservableProperty]
        private ICommand _command;

        public MenuItemViewModel(string? title, string? subtitle, ICommand command)
        {
            _title = title;
            _subtitle = subtitle;
            _command = command;
        }
    }
}