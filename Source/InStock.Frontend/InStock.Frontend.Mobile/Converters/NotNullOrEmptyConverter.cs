namespace InStock.Frontend.Mobile.Converters
{
    internal class NotNullOrEmptyConverter : Base.BaseValueConverter<string, bool>
    {
        protected override bool Convert(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}