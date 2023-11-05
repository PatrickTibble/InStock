using InStock.Common.Models.Base;

namespace InStock.Common.Models.Account.Login
{
	public class Response : IResponse
	{
        public static object? Default { get; set; }
        public bool IsSuccessfulStatusCode { get; set; }
        public string? AccessToken { get; set; }
    }
}