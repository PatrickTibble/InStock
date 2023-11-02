using System.ComponentModel.DataAnnotations;

namespace InStock.Common.Models.Base
{
    public interface IRequest
	{
		bool IsValid { get; }
	}
}