namespace InStock.Common.AccountService.Abstraction.Entities
{
    public class UserSession
    {
        public string? Username { get; set; }
        public Guid SessionId { get; set; }
        public string? AccessToken { get; set; }
    }
}