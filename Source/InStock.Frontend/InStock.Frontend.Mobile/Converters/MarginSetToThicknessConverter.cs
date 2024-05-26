using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Mobile.Converters.Base;

namespace InStock.Frontend.Mobile.Converters;

public class MarginSetToThicknessConverter : BaseValueConverter<MarginSet, Thickness>
{
    protected override Thickness Convert(MarginSet value)
    {
        return new Thickness(value.Left, value.Top, value.Right, value.Bottom);
    }
}
