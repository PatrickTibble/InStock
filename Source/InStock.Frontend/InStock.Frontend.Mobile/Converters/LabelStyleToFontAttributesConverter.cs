using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Core.ViewModels.Labels;
using InStock.Frontend.Mobile.Converters.Base;

namespace InStock.Frontend.Mobile.Converters;
public class LabelStyleToFontAttributesConverter : BaseValueConverter<LabelStyle, FontAttributes>
{
    protected override FontAttributes Convert(LabelStyle value)
    {
        return value switch
        {
            LabelStyle.Default => FontAttributes.None,
            LabelStyle.TitleLight => FontAttributes.Bold,
            LabelStyle.TitleDark => FontAttributes.Bold,
            LabelStyle.SubtitleLight => FontAttributes.None,
            LabelStyle.SubtitleDark => FontAttributes.None,
            LabelStyle.HeaderLight => FontAttributes.Bold,
            LabelStyle.HeaderDark => FontAttributes.Bold,
            LabelStyle.BodyLight => FontAttributes.None,
            LabelStyle.BodyDark => FontAttributes.None,
            LabelStyle.CaptionLight => FontAttributes.Italic,
            LabelStyle.CaptionDark => FontAttributes.Italic,
            LabelStyle.ErrorLight => FontAttributes.Bold,
            LabelStyle.ErrorDark => FontAttributes.Bold,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}
