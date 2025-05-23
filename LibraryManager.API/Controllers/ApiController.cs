namespace LibraryManager.API.Controllers
{
    using LibraryManager.Core.Common;
    using Microsoft.AspNetCore.Mvc;

    public class ApiController : ControllerBase
    {
        protected IActionResult HandleFailure(Result result) 
        {
            return result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Error,
                            validationResult.Errors)),
                _ => 
                BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Error))
            };
        }

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null)
        {
            return new()
            {
                Title = title,
                Status = status,
                Detail = error.Description,
                Extensions = { { nameof(errors), errors } }
            };
        }
    }
}
