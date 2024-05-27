using InStock.Frontend.Mobile.Converters.Base;

namespace InStock.Frontend.Mobile.Converters;

public class BoolToColorConverter : BaseValueConverter<bool, Color>
{
    public Color TrueColor { get; set; }
    public Color FalseColor { get; set; }

    protected override Color Convert(bool value)
    {
        if (value)
        {
            return TrueColor;
        }
        return FalseColor;
    }
}
