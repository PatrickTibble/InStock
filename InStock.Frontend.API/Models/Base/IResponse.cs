namespace InStock.Frontend.API.Models.Base
{
	public interface IResponse
	{
		bool IsSuccessfulStatusCode { get; }
	}
}