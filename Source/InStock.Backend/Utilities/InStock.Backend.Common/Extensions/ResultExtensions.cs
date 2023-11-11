using InStock.Common.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Backend.Common.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result) where T : class
        {
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
    }
}