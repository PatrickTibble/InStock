using Microsoft.AspNetCore.Mvc;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Common.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Common.IdentityService.Abstraction.TransferObjects.UserClaims;
using InStock.Backend.Common.Extensions;

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
        [Route(Constants.UserClaims)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserClaimsAsync(
            [FromBody] UserClaimsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _identityService
                .GetUserClaimsAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route(Constants.Authenticate)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync(
            [FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _identityService
                .AuthenticateAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route(Constants.Register)]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _identityService
                .RegisterUserAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }
    }
}