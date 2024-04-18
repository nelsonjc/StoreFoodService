using Microsoft.AspNetCore.Mvc;
using ShopFood.Domain.DTOs.Results;
using System.Net;

namespace ShopFood.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Get Response Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<ObjectResult> GetResponseAsync<T>(HttpStatusCode statusCode, string message, T response)
        {
            return await Task.Run(() =>
            {
                var objectResult = new HttpResponse<T>((int)statusCode, message, response);
                return new ObjectResult(objectResult)
                {
                    StatusCode = (int)statusCode
                };
            });
        }
    }
}
