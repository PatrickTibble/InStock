using InStock.Common.IdentityService.Abstraction.Entities;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Base;
using System.ComponentModel.DataAnnotations;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate
{
    public class AuthenticationRequest : BaseRequest
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