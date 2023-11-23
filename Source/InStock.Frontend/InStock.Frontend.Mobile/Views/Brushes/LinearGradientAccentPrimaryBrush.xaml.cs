using InStock.Frontend.Abstraction.Enums;

namespace InStock.Frontend.Mobile.Views.Brushes;

public partial class LinearGradientAccentPrimaryBrush : LinearGradientBrush
{
    public static readonly BindableProperty DirectionProperty = BindableProperty.Create(
        nameof(Direction),
        typeof(LinearGradientDirection),
        typeof(LinearGradientAccentPrimaryBrush),
        LinearGradientDirection.TopToBottom,
        propertyChanged: (b, o, n) => ((LinearGradientAccentPrimaryBrush)b).OnDirectionChanged((LinearGradientDirection)o, (LinearGradientDirection)n));

    public LinearGradientDirection Direction
    {
        get => (LinearGradientDirection)GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }

    public LinearGradientAccentPrimaryBrush()
    {
        InitializeComponent();
    }

    private void OnDirectionChanged(LinearGradientDirection o, LinearGradientDirection n)
    {
        if (o == n) { return; }

        switch (n)
        {
            case LinearGradientDirection.TopToBottom:
                StartPoint = new Point(0.5, 0);
                EndPoint = new Point(0.5, 1);
                break;
            case LinearGradientDirection.LeftToRight:
                StartPoint = new Point(0, 0.5);
                EndPoint = new Point(1, 0.5);
                break;
            case LinearGradientDirection.RightToLeft:
                StartPoint = new Point(1, 0.5);
                EndPoint = new Point(0, 0.5);
                break;
            case LinearGradientDirection.BottomToTop:
                StartPoint = new Point(0.5, 1);
                EndPoint = new Point(0.5, 0);
                break;
        }
    }
}