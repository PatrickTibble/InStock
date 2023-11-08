using InStock.Common.IdentityService.Abstraction.Entities;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims
{
    public class UserClaimsResponse
    {
        public IList<string>? Claims { get; set; }
        public string? Username { get; set; }
    }
}