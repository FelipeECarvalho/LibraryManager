namespace LibraryManager.API.Exceptions
{
    using FluentValidation;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class ValidationExceptionHandler (
        IProblemDetailsService problemDetailsService,
        ILogger<ValidationExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
            {
                return false;
            }

            logger.LogError(exception, "A validation exception occurred");

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var context = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Detail = "One or more validation errors occurred",
                    Status = StatusCodes.Status400BadRequest
                }
            };

            var errors = validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key.ToLowerInvariant(),
                    g => g.Select(c => c.ErrorMessage).ToArray()
                );

            context.ProblemDetails.Extensions.Add("errors", errors);

            return await problemDetailsService.TryWriteAsync(context);
        }
    }
}
