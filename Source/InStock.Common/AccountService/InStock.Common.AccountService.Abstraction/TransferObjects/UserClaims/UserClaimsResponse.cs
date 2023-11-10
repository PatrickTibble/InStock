using InStock.Common.AccountService.Abstraction.Entities;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.UserClaims
{
    public class UserClaimsResponse
    {
        public IList<string>? Claims { get; set; }
        public string? Username { get; set; }
    }
}