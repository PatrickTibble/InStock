namespace InStock.Common.Models.Base
{
    public class BaseResponse : IResponse
	{
        public bool IsSuccessfulStatusCode { get; set; }
    }
}