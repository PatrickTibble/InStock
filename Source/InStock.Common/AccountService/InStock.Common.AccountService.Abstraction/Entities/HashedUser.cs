namespace InStock.Common.AccountService.Abstraction.Entities
{
    public class HashedUser
    {
        public string? Username { get; set; }
        public UserRole Role { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}