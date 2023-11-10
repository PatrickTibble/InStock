using InStock.Frontend.Mobile.Converters.Base;

namespace InStock.Frontend.Mobile.Converters
{
    public class InverseBoolConverter : BaseValueConverter<bool, bool>
    {
        protected override bool Convert(bool value) => !value;
        protected override bool ConvertBack(bool value) => !value;
    }
}
