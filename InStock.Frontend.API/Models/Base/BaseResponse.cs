namespace InStock.Frontend.API.Models.Base
{
    public class BaseResponse : IResponse
	{
        public bool IsSuccessfulStatusCode { get; protected set; }
    }
}