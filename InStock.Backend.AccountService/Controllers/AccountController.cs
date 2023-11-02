using InStock.Backend.AccountService.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace InStock.Backend.AccountService.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IAccountService _accountService;

        public AccountController(
            IIdentityService identityService,
            IAccountService accountService)
        {
            _identityService = identityService;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("api/v1/Account/SessionState")]
        [SwaggerOperation(Description = "Returns user session state")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Common.Models.Account.SessionStatus.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSessionStateAsync(
            [FromHeader(Name = "accessToken")] string accessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = await _identityService.GetUserClaimsAsync(accessToken);
            if (!claims.Any() || !claims.Contains(UserClaim.Session_Read))
            {
                return Unauthorized();
            }

            var response = await _accountService.GetSessionStateAsync(accessToken);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/v1/Account/Login")]
        [SwaggerOperation(Description = "Attempt account log in with credentials")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Common.Models.Account.Login.Response), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync(
            [FromBody] string username,
            [FromBody] string password,
            [FromBody] IList<string> claims)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessToken = await _identityService.VerifyUserCredentialsAsync(username, password, claims);

            if (string.IsNullOrEmpty(accessToken))
            {
                return Ok(Common.Models.Account.Login.Response.Default);
            }

            return Ok(new Common.Models.Account.Login.Response
            {
                IsSuccessfulStatusCode = true,
                AccessToken = accessToken
            });
        }
    }
}