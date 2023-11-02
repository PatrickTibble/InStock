namespace InStock.Common.Models.Base
{
	public interface IResponse
	{
		bool IsSuccessfulStatusCode { get; }
	}
}