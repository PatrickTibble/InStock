using InStock.Backend.IdentityService.Abstraction.Entities;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.UserClaims
{
    public class UserClaimsResponse
    {
        public IList<string>? Claims { get; set; }
        public string? Username { get; set; }
    }
}