namespace InStock.Common.AccountService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationResponse
    {
        public string? IdToken { get; set; }
        public string? AccessToken { get; set; }
    }
}