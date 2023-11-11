namespace InStock.Common.Models.Base
{
    public class Result<T> where T : class
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public Result()
        {

        }

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
