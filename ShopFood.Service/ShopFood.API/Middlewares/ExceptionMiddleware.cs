using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Interfaces.Logger;
using ShopFood.Domain.Variables;
using System.Net;
using System.Web.Http;

namespace ShopFood.API.Middlewares
{
    public class ExceptionMiddleware
    {
        #region Properties

        private readonly RequestDelegate _next;

        #endregion Properties

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion Constructor

        #region Public

        /// <summary>
        /// Invoke Async
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, IShopFoodLogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpResponseException ex)
            {
                await logger.SetErrorLog(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await logger.SetErrorLog(ex);
                await HandleExceptionAsync(httpContext, ex);
            }

        }
        #endregion Public

        #region Private

        /// <summary>
        /// Handle Exception Async
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new HttpErrorResponse()
            {
                StatusCode = context.Response.StatusCode,
                Description = ServiceMessages.ERROR,
                ExceptionMessage = exception.Message
            }.ToString());
        }


        /// <summary>
        /// Handle Exception Async
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, HttpResponseException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Response.StatusCode;

            await context.Response.WriteAsync(new HttpErrorResponse()
            {
                StatusCode = context.Response.StatusCode,
                Description = exception.Response.ReasonPhrase,
                ExceptionMessage = exception.Message
            }.ToString());
        }
        #endregion Private
    }
}
