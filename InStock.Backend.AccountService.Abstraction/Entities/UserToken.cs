using InStock.Backend.AccountService.Abstraction.Services;

namespace InStock.Backend.AccountService.Abstraction.Entities
{
    public class UserToken
    {
        public DateTime Expiry { get; set; }
        public IList<UserClaim> Claims { get; set; }
    }
}