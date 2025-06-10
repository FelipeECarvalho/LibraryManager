namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/library/{libraryId:guid}/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected Library Library { get => GetLibraryFromRoute(); }

        protected IActionResult HandleFailure(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return result switch
            {
                IValidationResult validationResult => BadRequest(CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors)),

                _ when result.Error.Type is ErrorType.NotFound => NotFound(CreateProblemDetails(
                    "Not Found",
                    StatusCodes.Status404NotFound,
                    result.Error)),

                _ => BadRequest(CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error))
            };
        }

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[] errors = null)
        {
            return new()
            {
                Title = title,
                Status = status,
                Detail = error.Description,
                Extensions = { { nameof(errors), errors } }
            };
        }

        private Library GetLibraryFromRoute()
        {
            var routeValue = RouteData.Values["libraryId"];

            if (routeValue != null && 
                Guid.TryParse(routeValue.ToString(), out var id))
            {
                return new Library("Teste", null, null, null);
            }

            throw new InvalidOperationException("libraryId não foi encontrado na rota.");
        }
    }
}
