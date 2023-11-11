namespace InStock.Common.AccountService.Abstraction.TransferObjects.Login
{
	public class LoginResponse
	{
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}