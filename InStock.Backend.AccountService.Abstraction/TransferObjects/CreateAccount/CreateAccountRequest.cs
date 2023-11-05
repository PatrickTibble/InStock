namespace InStock.Backend.AccountService.Abstraction.TransferObjects.CreateAccount
{
    public class CreateAccountRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}