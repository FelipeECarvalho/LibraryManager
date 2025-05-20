namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.User.CreateUser;
    using LibraryManager.Application.Commands.User.DeleteUser;
    using LibraryManager.Application.Commands.User.UpdateUser;
    using LibraryManager.Application.Queries.User.GetUserById;
    using LibraryManager.Application.Queries.User.GetUserLoans;
    using LibraryManager.Application.Queries.User.GetUsers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUsersQuery(), ct);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id), ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var user = result.Value;

            return Ok(user);
        }

        [HttpGet("{id:guid}/loans")]
        public async Task<IActionResult> GetLoans(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserLoansQuery(id), ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var loans = result.Value;

            return Ok(loans);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var user = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id), ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateUserCommand command,
            CancellationToken ct)
        {
            command.Id = id;
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
