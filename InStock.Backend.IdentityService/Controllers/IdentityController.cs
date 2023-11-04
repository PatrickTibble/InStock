using Microsoft.AspNetCore.Mvc;
using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Abstraction;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;
using InStock.Backend.IdentityService.Abstraction.Entities;

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
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync(
            [FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = new List<UserClaim>();
            var response = await _identityService.AuthenticateAsync(request, claims);
            return Ok(response);
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

            var response = await _identityService.RegisterUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route(Constants.SendVerificationLink)]
        [ProducesResponseType(typeof(VerificationLinkResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SendVerificationLinkAsync(
            [FromBody] VerificationLinkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _identityService.SendVerificationLinkAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route(Constants.VerifyEmail)]
        [ProducesResponseType(typeof(VerifyEmailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyEmailAsync(
            [FromBody] VerifyEmailRequest request)
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