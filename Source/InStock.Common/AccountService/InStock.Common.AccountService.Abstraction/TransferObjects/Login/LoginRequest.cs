using System.ComponentModel.DataAnnotations;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Login
{
    public class LoginRequest
	{
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters")]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Client ID is required")]
        public Guid ClientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Client name is required")]
        [MinLength(3, ErrorMessage = "Client name must be at least 3 characters")]
        public string? ClientName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Client description is required")]
        [MinLength(10, ErrorMessage = "Client name must be at least 10 characters")]
        public string? ClientDescription { get; set; }
    }
}