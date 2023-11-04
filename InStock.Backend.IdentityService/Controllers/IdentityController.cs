using Microsoft.AspNetCore.Mvc;
using InStock.Backend.IdentityService.Abstraction.Services;
using Authenticate = InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using Register = InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using SendVerificationLink = InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using VerifyEmail = InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;
using InStock.Backend.IdentityService.Abstraction;

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
        [Route(Constants.Authenticate)]
        [ProducesResponseType(typeof(Authenticate.AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync(
            [FromBody] Authenticate.AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = new List<string>();
            var response = await _identityService.AuthenticateAsync(request, claims);
            return Ok(response);
        }

        [HttpPost]
        [Route(Constants.Register)]
        [ProducesResponseType(typeof(Register.RegistrationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] Register.RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _identityService.RegisterUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route(Constants.SendVerificationLink)]
        [ProducesResponseType(typeof(SendVerificationLink.VerificationLinkResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SendVerificationLinkAsync(
            [FromHeader] string accessToken,
            [FromBody] SendVerificationLink.VerificationLinkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = await _identityService.GetUserClaimsAsync(accessToken);
            if (claims == null || !claims.Any())
            {
                return Unauthorized("Invalid access token");
            }

            var response = await _identityService.SendVerificationLinkAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route(Constants.VerifyEmail)]
        [ProducesResponseType(typeof(VerifyEmail.VerifyEmailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyEmailAsync(
            [FromBody] VerifyEmail.VerifyEmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _identityService.VerifyEmailAsync(request);
            return Ok(response);
        }
    }
}