using InStock.Backend.IdentityService.Abstraction.Services;

namespace InStock.Backend.IdentityService.Abstraction.Entities
{
    public class UserToken
    {
        public string? Role { get; set; }
        public DateTime Expiry { get; set; }
        public IList<UserClaim>? Claims { get; set; }
        public string? Username { get; set; }
    }
}