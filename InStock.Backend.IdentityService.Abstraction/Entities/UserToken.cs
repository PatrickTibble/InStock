using InStock.Backend.AccountService.Abstraction.Services;

namespace InStock.Backend.AccountService.Abstraction.Entities
{
    public class UserToken
    {
        public string Role { get; set; }
        public DateTime Expiry { get; set; }
        public IList<UserClaim> Claims { get; set; }
        public string Username { get; set; }
    }
}