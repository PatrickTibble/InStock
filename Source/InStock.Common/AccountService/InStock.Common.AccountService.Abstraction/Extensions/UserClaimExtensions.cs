using InStock.Common.AccountService.Abstraction.Entities;

namespace InStock.Common.AccountService.Abstraction.Extensions
{
    public static class UserClaimExtensions
    {
        public static string ToClaimType(this UserClaim claim)
            => claim.ToString().Replace("_", ".").ToLower();
    }
}