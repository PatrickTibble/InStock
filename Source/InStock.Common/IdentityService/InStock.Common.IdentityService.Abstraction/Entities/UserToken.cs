namespace InStock.Common.IdentityService.Abstraction.Entities
{
    public class UserToken
    {
        public DateTime Expiry { get; set; }
        public IDictionary<string, string>? Claims { get; set; }
    }
}