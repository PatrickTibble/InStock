using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Core.ViewModels.Labels;
using InStock.Frontend.Mobile.Converters.Base;

namespace InStock.Frontend.Mobile.Converters;
public class LabelStyleToFontSizeConverter : BaseValueConverter<LabelStyle, double>
{
    protected override double Convert(LabelStyle value)
    {
        return value switch
        {
            LabelStyle.Default => 14,
            LabelStyle.TitleLight => 24,
            LabelStyle.TitleDark => 24,
            LabelStyle.HeaderLight => 20,
            LabelStyle.HeaderDark => 20,
            LabelStyle.SubtitleLight => 18,
            LabelStyle.SubtitleDark => 18,
            LabelStyle.BodyLight => 16,
            LabelStyle.BodyDark => 16,
            LabelStyle.CaptionLight => 14,
            LabelStyle.CaptionDark => 14,
            LabelStyle.ErrorLight => 16,
            LabelStyle.ErrorDark => 16,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}
