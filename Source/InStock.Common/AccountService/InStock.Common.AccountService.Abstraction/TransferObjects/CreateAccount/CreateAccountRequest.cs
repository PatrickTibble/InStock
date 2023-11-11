using System.ComponentModel.DataAnnotations;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount
{
    public class CreateAccountRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "First name must be at most 20 characters")]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "Last name must be at most 20 characters")]
        public string? LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [MinLength(7, ErrorMessage = "Username must be at least 3 characters")]
        [MaxLength(20, ErrorMessage = "Username must be at most 20 characters")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 7 characters")]
        [MaxLength(50, ErrorMessage = "Password must be at most 50 characters")]
        public string? Password { get; set; }
        public string ClientId { get; set; }
    }
}