using System.ComponentModel.DataAnnotations;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Register
{
    public class RegistrationRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters long.")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters long.")]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters long.")]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters long.")]
        public string? LastName { get; set; }
    }
}