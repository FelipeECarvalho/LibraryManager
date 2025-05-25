namespace LibraryManager.API.Middleware
{
    using LibraryManager.API.Models;
    using System.Net;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception is InvalidOperationException 
                ? (int)HttpStatusCode.BadRequest
                : (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
