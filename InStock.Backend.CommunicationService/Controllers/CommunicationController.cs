using Microsoft.AspNetCore.Mvc;

namespace InStock.Backend.CommunicationService.Controllers
{
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        [HttpGet]
        [Route("api/v1/Communication")]
        public Task<IActionResult> GetCommunicationsAsync()
        {
            var response = Ok();
            return Task.FromResult<IActionResult>(response);
        }
    }
}
