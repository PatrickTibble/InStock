using InStock.Backend.AccountService.Abstraction.TransferObjects.Base;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Backend.AccountService.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result) where T : class
        {
            return result.StatusCode switch
            {
                StatusCodes.Status200OK => new OkObjectResult(result.Data),
                StatusCodes.Status400BadRequest => new BadRequestObjectResult(result.Error),
                StatusCodes.Status401Unauthorized => new UnauthorizedResult(),
                StatusCodes.Status404NotFound => new NotFoundResult(),
                StatusCodes.Status500InternalServerError => new ObjectResult(result.Error)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                _ => new BadRequestObjectResult(result.Error)
            };
        }
    }
}