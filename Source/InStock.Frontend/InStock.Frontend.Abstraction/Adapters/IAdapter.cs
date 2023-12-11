namespace InStock.Frontend.Abstraction.Adapters
{
    public interface IAdapter<in TIn, out TOut>
    {
        TOut Convert(TIn value);
    }
}