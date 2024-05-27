using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Core.ViewModels.Labels;
using InStock.Frontend.Mobile.Converters.Base;
using InStock.Frontend.Mobile.Extensions;

namespace InStock.Frontend.Mobile.Converters;

public class LabelStyleToTextColorConverter : BaseValueConverter<LabelStyle, Color>
{
    private Color Dark => Application.Current.Resources.GetValueOrDefault("Gray950", Colors.Black);
    private Color Light => Application.Current.Resources.GetValueOrDefault("Gray100", Colors.White);
    private Color Red => Application.Current.Resources.GetValueOrDefault("FlyoutHeaderEnd", Colors.Red);
    protected override Color Convert(LabelStyle value)
    {
        return value switch
        {
            LabelStyle.Default => Dark,
            LabelStyle.TitleLight => Dark,
            LabelStyle.TitleDark => Light,
            LabelStyle.SubtitleLight => Dark,
            LabelStyle.SubtitleDark => Light,
            LabelStyle.HeaderLight => Dark,
            LabelStyle.HeaderDark => Light,
            LabelStyle.BodyLight => Dark,
            LabelStyle.BodyDark => Light,
            LabelStyle.CaptionLight => Dark,
            LabelStyle.CaptionDark => Light,
            LabelStyle.ErrorLight => Red,
            LabelStyle.ErrorDark => Red,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}
