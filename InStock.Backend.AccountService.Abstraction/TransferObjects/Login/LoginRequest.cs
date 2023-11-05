namespace InStock.Backend.AccountService.Abstraction.TransferObjects.Login
{
    public class LoginRequest
	{
        public string? Username { get; set; }
        public string? Password { get; set; }
        public IList<string>? Claims { get; set; }
    }
}