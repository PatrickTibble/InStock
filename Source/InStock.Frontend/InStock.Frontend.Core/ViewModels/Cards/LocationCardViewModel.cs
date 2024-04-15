using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Frontend.Core.ViewModels.Base;
using System.Windows.Input;

namespace InStock.Frontend.Core.ViewModels.Cards
{
    public partial class LocationCardViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? _detailText;

        [ObservableProperty]
        private string? _imageSource;

        [ObservableProperty]
        private ICommand? _command;

        public LocationCardViewModel(Location location)
        {
            Title = location.Name;
            DetailText = location.Description;
            ImageSource = location.ImageUrl;
        }
    }
}