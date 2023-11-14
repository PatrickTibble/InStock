using InStock.Common.Models.Base;
using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Core.Extensions
{
    public static class ResultExtensions
    {
        public static bool IsSuccessfulStatusCode<T>(this Result<T> result)
            where T : class
            => result.StatusCode == 200;

        public static BooleanResult ToBooleanResult<T>(this Result<T> result)
            where T : class
            => new BooleanResult
            {
                Result = result.IsSuccessfulStatusCode(),
                ErrorMessage = result.Error
            };
    }
}