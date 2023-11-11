using System.Net.Mime;
using InStock.Common.AccountService.Abstraction;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Backend.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using InStock.Common.Models.Base;

namespace InStock.Backend.AccountService.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route(Constants.Login)]
        [SwaggerOperation(Description = "Attempt account log in with credentials")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService
                .LoginAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route("api/v1/Account/CreateAccount")]
        [SwaggerOperation(Description = "Create a User Account")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateUserAccountAsync(
            [FromBody] CreateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService
                .CreateAccountAsync(request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }
    }
}