using System.ComponentModel.DataAnnotations;

namespace InStock.Common.AccountService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationRequest 
    {
        [Required]
        [MinLength(8, ErrorMessage = "Username is too short")]
        public string? Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password is too short")]
        public string? Password { get; set; }
        public List<string>? Claims { get; set; }
    }
}