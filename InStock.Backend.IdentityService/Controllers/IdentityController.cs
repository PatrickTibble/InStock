using Microsoft.AspNetCore.Mvc;
using InStock.Backend.IdentityService.Abstraction.Services;
using Authenticate = InStock.Backend.IdentityService.Abstraction.TransferObjects.Authenticate;
using Register = InStock.Backend.IdentityService.Abstraction.TransferObjects.Register;
using SendVerificationLink = InStock.Backend.IdentityService.Abstraction.TransferObjects.SendVerificationLink;
using VerifyEmail = InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail;

namespace InStock.Backend.IdentityService.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("api/v1/Identity/Authenticate")]
        [ProducesResponseType(typeof(Authenticate.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthenticateAsync(
            [FromBody] Authenticate.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!request.IsValid)
            {
                return BadRequest(request.ValidationErrors);
            }

            var claims = new List<string>();
            var response = await _identityService.VerifyUserCredentialsAsync(request, claims);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/v1/Identity/Register")]
        [ProducesResponseType(typeof(Register.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] Register.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!request.IsValid)
            {
                return BadRequest(request.ValidationErrors);
            }

            var response = await _identityService.RegisterUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/v1/Identity/SendVerificationLink")]
        [ProducesResponseType(typeof(SendVerificationLink.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendVerificationLinkAsync(
            [FromBody] SendVerificationLink.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!request.IsValid)
            {
                return BadRequest(request.ValidationErrors);
            }

            var response = await _identityService.SendVerificationLinkAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/v1/Identity/VerifyEmail")]
        [ProducesResponseType(typeof(VerifyEmail.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyEmailAsync(
            [FromBody] VerifyEmail.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!request.IsValid)
            {
                return BadRequest(request.ValidationErrors);
            }

            var response = await _identityService.VerifyEmailAsync(request);
            return Ok(response);
        }
    }
}