using System.ComponentModel.DataAnnotations;

namespace InStock.Frontend.API.Models.Base
{
    public interface IRequest
	{
		bool IsValid { get; }
	}
}