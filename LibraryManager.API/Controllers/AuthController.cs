namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Auth.Login;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IMediator _mediator) : ApiControllerBase
    {
        [HttpPut("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(
            LoginCommand command,
            CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }
    }
}
