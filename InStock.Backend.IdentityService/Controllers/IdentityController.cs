using Microsoft.AspNetCore.Mvc;
using InStock.Backend.Common.Extensions;
using InStock.Common.IdentityService.Abstraction;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.GetToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.RefreshToken;
using InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ValidateToken;

namespace InStock.Backend.IdentityService.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(
            IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route(Constants.GetToken)]
        [ProducesResponseType(typeof(GetTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTokenAsync(
            [FromBody] GetTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _identityService
                .GetTokenAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route(Constants.ValidateToken)]
        [ProducesResponseType(typeof(ValidateTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateTokenAsync(
                       [FromBody] ValidateTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _identityService
                .ValidateTokenAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route(Constants.RefreshToken)]
        [ProducesResponseType(typeof(AccessRefreshTokenPair), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshTokenAsync(
                       [FromBody] AccessRefreshTokenPair request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _identityService
                .RefreshTokenAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }
    }
}