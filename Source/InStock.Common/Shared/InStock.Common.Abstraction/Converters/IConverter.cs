namespace InStock.Common.Abstraction.Converters
{
    public interface IConverter<in TIn>
    {
        TOut Convert<TOut>(TIn input);
    }
}