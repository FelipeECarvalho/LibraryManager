namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.User.CreateUser;
    using LibraryManager.Application.Commands.User.DeleteUser;
    using LibraryManager.Application.Commands.User.UpdateUser;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Application.Queries.Loan;
    using LibraryManager.Application.Queries.User;
    using LibraryManager.Application.Queries.User.GetUserById;
    using LibraryManager.Application.Queries.User.GetUserLoans;
    using LibraryManager.Application.Queries.User.GetUsers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// An user
    /// </summary>
    [Obsolete("Delete later because I don't want to expose user functionalities through API")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ApiControllerBase
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <response code="200">Users retrieved successfully.</response>
        /// <returns>A list containing all users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUsersQuery(), ct);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves the user with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <response code="200">User retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        /// <returns>Returns the user if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id), ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var user = result.Value;

            return Ok(user);
        }

        /// <summary>
        /// Retrieves the user loans with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <response code="200">User retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        /// <returns>Returns the user loans if found.</returns>
        [HttpGet("{id:guid}/loans")]
        [ProducesResponseType(typeof(IList<LoanResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLoans(
            Guid id,
            CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserLoansQuery(id), ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var loans = result.Value;

            return Ok(loans);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userRequest">An object containing the user's data.</param>
        /// <response code="201">User created successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <returns>The newly created user.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] CreateUserCommand userRequest,
            CancellationToken ct)
        {
            var result = await _mediator.Send(userRequest, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var user = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Deletes an user by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <response code="204">User deleted successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id), ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates an user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="userRequest">An object containing the updated user data.</param>
        /// <response code="204">User updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">User not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateUserCommand userRequest,
            CancellationToken ct)
        {
            userRequest.Id = id;
            var result = await _mediator.Send(userRequest, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}
