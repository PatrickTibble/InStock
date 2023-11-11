using System.ComponentModel.DataAnnotations;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Login
{
    public class LoginRequest
	{
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string ClientId { get; set; }
    }
}