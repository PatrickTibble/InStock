using System.Security.Claims;
using InStock.Backend.IdentityService.Abstraction.Services;

namespace InStock.Backend.IdentityService.Data.Extensions
{
    public static class ClaimExtensions
    {
        public static UserClaim ToUserClaim(this Claim claim)
        {
            // TODO: Implement this.
            return UserClaim.Session_Read; // session.read
        }
    }
}