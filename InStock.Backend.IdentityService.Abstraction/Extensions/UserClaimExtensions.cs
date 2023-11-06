using InStock.Common.IdentityService.Abstraction.Entities;

namespace InStock.Common.IdentityService.Abstraction.Extensions
{
    public static class UserClaimExtensions
    {
        public static string ToClaimType(this UserClaim claim)
            => claim.ToString().Replace("_", ".").ToLower();
    }
}