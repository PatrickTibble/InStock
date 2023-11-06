namespace InStock.Backend.AccountService.Abstraction.TransferObjects.Base
{
    public class Result<T> where T : class
    {
        public int StatusCode { get; }
        public T? Data { get; }
        public string? Error { get; }

        public Result(int statusCode, T? data, string? error)
        {
            StatusCode = statusCode;
            Data = data;
            Error = error;
        }

        public Result(int statusCode, T? data) : this(statusCode, data, null)
        {

        }

        public Result(int statusCode, string? error) : this(statusCode, null, error)
        {

        }

        public Result(T? data) : this(200, data, null)
        {

        }
    }
}
