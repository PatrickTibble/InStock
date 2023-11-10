using System.Globalization;

namespace InStock.Frontend.Mobile.Converters.Base
{
    public abstract class BaseValueConverter<TIn, TOut> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Convert((TIn)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertBack((TOut)value);

        protected abstract TOut Convert(TIn value);

        protected virtual TIn ConvertBack(TOut value)
        {
            throw new NotImplementedException();
        }
    }
}