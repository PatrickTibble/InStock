using CommunityToolkit.Mvvm.ComponentModel;
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
        private ObservableCollection<ChartPoint>? _points;

        public EventHandler? Draw { get; set; }

        public override Task InitializeAsync(object? navigationData = null)
        {
            if (navigationData is ChartDataSet dataSet
                && dataSet.Points is IList<ChartPoint> points 
                && dataSet.Axis.X.Names is IList<string> xAxis
                && dataSet.Axis.Y.Names is IList<string> yAxis)
            {
                Points = new ObservableCollection<ChartPoint>(points);
                XAxis = new ObservableCollection<string>(xAxis);
                YAxis = new ObservableCollection<string>(yAxis);
            }

            Draw?.Invoke(null, EventArgs.Empty);

            return base.InitializeAsync(navigationData);
        }
    }
}
