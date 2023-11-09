using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using System.ComponentModel.DataAnnotations;

namespace InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken
{
    public class GetTokenResponse : AccessRefreshTokenPair
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identity token is required.")]
        public string IdentityToken { get; set; } = string.Empty;
    }
}