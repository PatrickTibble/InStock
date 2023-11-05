using System.Net.Mime;
using InStock.Backend.AccountService.Abstraction;
using InStock.Backend.AccountService.Abstraction.Services;
using InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState;
using InStock.Backend.IdentityService.Abstraction.Entities;
using InStock.Backend.IdentityService.Abstraction.Extensions;
using InStock.Backend.IdentityService.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [Route(Constants.SessionState)]
        [SwaggerOperation(Description = "Returns user session state")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SessionStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSessionStateAsync(
            [FromHeader] string? accessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = await _identityService.GetUserClaimsAsync(new IdentityService.Abstraction.TransferObjects.UserClaims.UserClaimsRequest
            {
                AccessToken = accessToken
            });

            if (!claims.Any() || !claims.Contains(UserClaim.Session_Read.ToClaimType()))
            {
                return Unauthorized();
            }

            var response = await _accountService.GetSessionStateAsync(accessToken);

            return Ok(response);
        }
        
        //[HttpGet]
        //[Route("api/v1/Account/Login")]
        //[SwaggerOperation(Description = "Attempt account log in with credentials")]
        //[Produces(MediaTypeNames.Application.Json)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(typeof(Common.Models.Account.Login.Response), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> LoginAsync(
        //    [FromBody] Common.Models.Account.Login.Request request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!request.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var response = await _identityService.AuthenticateAsync(
        //        request: new IdentityService.Abstraction.TransferObjects.Authenticate.AuthenticationRequest
        //        {
        //            Username = request.Username,
        //            Password = request.Password
        //        },
        //        claims: new List<UserClaim>
        //        {
        //            UserClaim.Session_Read
        //        }
        //    );

        //    if (string.IsNullOrEmpty(response.AccessToken))
        //    {
        //        return Ok(Common.Models.Account.Login.Response.Default);
        //    }

        //    return Ok(new Common.Models.Account.Login.Response
        //    {
        //        IsSuccessfulStatusCode = true,
        //        AccessToken = response.AccessToken
        //    });
        //}

        //[HttpPost]
        //[Route("api/v1/Account/CreateAccount")]
        //[SwaggerOperation(Description = "Create a User Account")]
        //[Produces(MediaTypeNames.Application.Json)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(typeof(Common.Models.Account.CreateAccount.Response), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<IActionResult> CreateUserAccountAsync(
        //    [FromHeader(Name = "accessToken")] string accessToken,
        //    [FromBody] Common.Models.Account.CreateAccount.Request request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!request.IsValid)
        //    {
        //        return BadRequest();
        //    }


        //    var claims = await _identityService.GetUserClaimsAsync(accessToken);
        //    if (!claims.Any() || !claims.Contains(UserClaim.Account_Create))
        //    {
        //        return Unauthorized();
        //    }

        //    var result = await _accountService.CreateAccountAsync(request);
        //    return Ok(result);
        //}
    }
}