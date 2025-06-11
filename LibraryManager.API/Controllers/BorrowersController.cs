namespace LibraryManager.API.Controllers
{
    using LibraryManager.Application.Commands.Borrower.CreateBorrower;
    using LibraryManager.Application.Commands.Borrower.DeleteBorrower;
    using LibraryManager.Application.Commands.Borrower.UpdateBorrower;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Application.Queries.Borrower;
    using LibraryManager.Application.Queries.Borrower.GetBorrowerById;
    using LibraryManager.Application.Queries.Borrower.GetBorrowers;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A borrower
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Operator")]
    public class BorrowersController(IMediator _mediator) : ApiControllerBase
    {
        /// <summary>
        /// Retrieves all borrowers.
        /// </summary>
        /// <response code="200">Borrowers retrieved successfully.</response>
        /// <returns>A list containing all borrowers.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<BorrowerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetBorrowersQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves the borrower with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower.</param>
        /// <response code="200">Borrower retrieved successfully.</response>
        /// <response code="404">Borrower not found.</response>
        /// <returns>Returns the borrower if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BorrowerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBorrowerByIdQuery(id), cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var borrower = result.Value;

            return Ok(borrower);
        }

        /// <summary>
        /// Creates a new borrower
        /// </summary>
        /// <param name="borrowerRequest">An object containing the borrower's data.</param>
        /// <response code="201">Borrower created successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <returns>The newly created borrower.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] CreateBorrowerCommand borrowerRequest,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(borrowerRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var borrower = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = borrower.Id }, borrower);
        }

        /// <summary>
        /// Deletes an borrower by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower.</param>
        /// <response code="204">Borrower deleted successfully.</response>
        /// <response code="404">Borrower not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteBorrowerCommand(id), cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates an borrower.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower.</param>
        /// <param name="borrowerRequest">An object containing the updated borrower data.</param>
        /// <response code="204">Borrower updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">Borrower not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateBorrowerCommand borrowerRequest,
            CancellationToken cancellationToken)
        {
            borrowerRequest.Id = id;
            var result = await _mediator.Send(borrowerRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}
