﻿using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Core.ViewModels.Base;

namespace InStock.Frontend.Core.ViewModels.Input
{
    public partial class PrimaryEntryViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? _text;

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