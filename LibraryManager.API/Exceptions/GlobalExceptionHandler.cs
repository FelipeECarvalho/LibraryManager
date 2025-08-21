namespace LibraryManager.API.Exceptions
{
    using LibraryManager.Application.Abstractions;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GlobalExceptionHandler(
        IServiceScopeFactory serviceScopeFactory,
        IProblemDetailsService problemDetailsService)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var logger = scope.ServiceProvider
                .GetRequiredService<IAppLogger<GlobalExceptionHandler>>();

            logger.LogError(exception, "An unexpected error occurred: {Message}", exception.Message);

            httpContext.Response.StatusCode = exception switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "An error occured",
                    Detail = exception.Message
                }
            });
        }
    }
}
