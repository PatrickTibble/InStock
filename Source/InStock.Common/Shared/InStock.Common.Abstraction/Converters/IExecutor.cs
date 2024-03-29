﻿namespace InStock.Common.Abstraction.Converters
{
    public interface IExecutor<TSource, TProduct>
    {
        IList<T> Execute<T>(TSource command, IConverter<TProduct> converter)
            where T : class;

        int Execute(TSource command);
    }
}