namespace InStock.Common.Abstraction.Converters
{
    public interface IExecutor<TSource, TProduct>
    {
        IList<T> Execute<T>(TSource command, IConverter<TProduct> converter);

        int Execute(TSource command);
    }
}