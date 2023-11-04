using InStock.Backend.IdentityService.Abstraction.Entities;

namespace InStock.Backend.IdentityService.Data.Extensions
{
    public static class UserClaimExtensions
    {
        public static string ToClaimType(this UserClaim claim)
            => claim.ToString().Replace("_", ".").ToLower();
    }
}