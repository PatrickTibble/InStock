﻿using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Drawing;

namespace InStock.Frontend.Core.ViewModels.Cards
{
    public partial class ChartViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<string>? _xAxis;

        [ObservableProperty]
        private ObservableCollection<string>? _yAxis;

        [ObservableProperty]
        private ObservableCollection<Point>? _points;

        public override Task InitializeAsync(object? navigationData = null)
        {
            if (navigationData is ChartDataSet dataSet)
            {

            }

            return base.InitializeAsync(navigationData);
        }
    }
}