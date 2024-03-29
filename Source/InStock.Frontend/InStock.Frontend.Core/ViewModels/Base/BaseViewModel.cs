﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace InStock.Frontend.Core.ViewModels.Base
{
    public abstract partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty]
		private string? _title;
	}
}