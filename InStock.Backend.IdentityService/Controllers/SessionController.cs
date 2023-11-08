using InStock.Common.IdentityService.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using InStock.Backend.Common.Extensions;
using InStock.Common.IdentityService.Abstraction;

namespace InStock.Backend.IdentityService.Controllers
{
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        [Route(Constants.SessionState)]
        public async Task<IActionResult> GetSessionStateAsync(
            [FromHeader] string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest();
            }

            var result = await _sessionService
                .GetSessionStateAsync(accessToken)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route(Constants.ContinueSession)]
        public async Task<IActionResult> ContinueSessionAsync(
            [FromHeader] string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest();
            }

            var result = await _sessionService
                .ContinueSessionAsync(accessToken)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }

        // Login should create the session... I think
        //[HttpPost]
        //[Route(Constants.CreateSession)]
        //public async Task<IActionResult> CreateSessionAsync(
        //               [FromHeader] string accessToken)
        //{
        //    if (!string.IsNullOrWhiteSpace(accessToken))
        //    {
        //        return BadRequest();
        //    }

        //    var result = await _sessionService
        //        .CreateSessionAsync(accessToken)
        //        .ConfigureAwait(false);

        //    return result.ToActionResult();
        //}

        [HttpPost]
        [Route(Constants.EndSession)]
        public async Task<IActionResult> EndSessionAsync(
                       [FromHeader] string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest();
            }

            var result = await _sessionService
                .EndSessionAsync(accessToken)
                .ConfigureAwait(false);

            return result.ToActionResult();
        }
    }
}