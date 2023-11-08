using Microsoft.AspNetCore.Mvc;
using InStock.Backend.Common.Extensions;

namespace InStock.Backend.AccountService.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UserController(
            IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route(Constants.UserAddress)]
        public async Task<IActionResult> AddUserAddressAsync(
            [FromHeader] string accessToken,
            [FromBody] AddAddressRequest request)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usersService
                .AddUserAddressAsync(accessToken, request)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpGet]
        [Route(Constants.UserAddresses)]
        public async Task<IActionResult> GetUserAddressesAsync(
            [FromHeader] string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest();
            }

            var result = await _usersService
                .GetUserAddressesAsync(accessToken)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpGet]
        [Route(Constants.UserProfile)]
        public async Task<IActionResult> GetUserProfileAsync(
            [FromHeader] string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest(); 
            }

            var result = await _usersService
                .GetUserProfileAsync(accessToken)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }
    }
}