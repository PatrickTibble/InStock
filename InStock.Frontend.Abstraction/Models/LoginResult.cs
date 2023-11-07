using InStock.Frontend.Abstraction.Models.Base;

namespace InStock.Frontend.Abstraction.Models
{
    public class LoginResult : BaseResult
	{
        public string? AccessToken { get; set; }
    }
}