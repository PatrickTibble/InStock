namespace InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken
{
    public class GetTokenRequest
    {
        public DateTime Expiry { get; set; }
        public IDictionary<string, string>? Claims { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}