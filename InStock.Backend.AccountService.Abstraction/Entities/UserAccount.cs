namespace InStock.Backend.AccountService.Abstraction.Entities
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}