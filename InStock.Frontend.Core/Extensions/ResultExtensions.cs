using InStock.Common.AccountService.Abstraction.TransferObjects.Base;

namespace InStock.Frontend.Core.Extensions
{
    public static class ResultExtensions
    {
        public static bool IsSuccessfulStatusCode<T>(this Result<T> result)
            where T : class
            => result.StatusCode == 200;
    }
}