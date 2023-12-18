using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Core.ViewModels.Cards;
using InStock.Frontend.Mobile.Drawables;

namespace InStock.Frontend.Mobile.Views.Graphics
{
    public class ChartView : GraphicsView
    {
        public static readonly BindableProperty PointsProperty = BindableProperty.Create(
            propertyName: nameof(Points),
            returnType: typeof(IList<ChartPoint>),
            declaringType: typeof(ChartView),
            propertyChanged: (b, o, n) => ((ChartView)b).Chart.Points = (IList<ChartPoint>)n);

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(
            propertyName: nameof(StartColor),
            returnType: typeof(Color),
            declaringType: typeof(ChartView),
            defaultValue: Colors.Black,
            propertyChanged: (b, o, n) => ((ChartView)b).Chart.StartColor = (Color)n);

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(
            propertyName: nameof(EndColor),
            returnType: typeof(Color),
            declaringType: typeof(ChartView),
            defaultValue: Colors.Black,
            propertyChanged: (b, o, n) => ((ChartView)b).Chart.EndColor = (Color)n);

        public IList<ChartPoint> Points
        {
            get => (IList<ChartPoint>)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        public ChartDrawable Chart => Drawable as ChartDrawable;

        public ChartView()
        {
            Drawable = new ChartDrawable();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is ChartViewModel vm)
            {
                vm.Draw += Draw;

                if (vm.Points != null && vm.XAxis != null && vm.YAxis != null)
                {
                    Draw(null, EventArgs.Empty);
                }
            }
        }

        private void Draw(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
