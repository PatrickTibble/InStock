using System.ComponentModel.DataAnnotations;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken
{
    public class AccessRefreshTokenPair
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Access token is required")]
        public string AccessToken { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}