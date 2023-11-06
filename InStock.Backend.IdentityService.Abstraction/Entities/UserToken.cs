using InStock.Common.IdentityService.Abstraction.Services;

namespace InStock.Common.IdentityService.Abstraction.Entities
{
    public class UserToken
    {
        public string? Role { get; set; }
        public DateTime Expiry { get; set; }
        public IList<string>? Claims { get; set; }
        public string? Username { get; set; }
    }
}