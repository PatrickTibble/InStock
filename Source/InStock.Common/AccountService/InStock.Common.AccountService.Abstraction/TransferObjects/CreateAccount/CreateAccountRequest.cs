using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using System.ComponentModel.DataAnnotations;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount
{
    public class CreateAccountRequest : LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "First name must be at most 20 characters")]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "Last name must be at most 20 characters")]
        public string? LastName { get; set; }
    }
}