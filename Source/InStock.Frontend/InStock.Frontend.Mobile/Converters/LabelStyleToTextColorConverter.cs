using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Core.ViewModels.Labels;
using InStock.Frontend.Mobile.Converters.Base;

namespace InStock.Frontend.Mobile.Converters;

public class LabelStyleToTextColorConverter : BaseValueConverter<LabelStyle, Color>
{
    protected override Color Convert(LabelStyle value)
    {
        return value switch
        {
            LabelStyle.Default => Colors.Black,
            LabelStyle.TitleLight => Color.FromArgb("#007bff"),
            LabelStyle.TitleDark => Color.FromArgb("#007bff"),
            LabelStyle.HeaderLight => Color.FromArgb("#28a745"),
            LabelStyle.HeaderDark => Color.FromArgb("#dc3545"),
            LabelStyle.SubtitleLight => Color.FromArgb("#17a2b8"),
            LabelStyle.SubtitleDark => Color.FromArgb("#ffc107"),
            LabelStyle.BodyLight => Color.FromArgb("#6c757d"),
            LabelStyle.BodyDark => Color.FromArgb("#6c757d"),
            LabelStyle.CaptionLight => Color.FromArgb("#6c757d"),
            LabelStyle.CaptionDark => Color.FromArgb("#6c757d"),
            LabelStyle.ErrorLight => Color.FromArgb("#dc3545"),
            LabelStyle.ErrorDark => Color.FromArgb("#dc3545"),
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}
