namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Loan.CreateLoan;
    using LibraryManager.Application.Commands.Loan.UpdateLoan;
    using LibraryManager.Application.Commands.Loan.UpdateLoanStatus;
    using LibraryManager.Application.Queries.Loan;
    using LibraryManager.Application.Queries.Loan.GetLoanById;
    using LibraryManager.Application.Queries.Loan.GetLoans;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A Loan
    /// </summary>
    /// <param name="_mediator"></param>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController(IMediator _mediator) : ApiControllerBase
    {
        /// <summary>
        /// Retrieves all loans.
        /// </summary>
        /// <response code="200">Loans retrieved successfully.</response>
        /// <returns>A list containing all loans.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<LoanResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetLoansQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves the loan with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the loan.</param>
        /// <response code="200">Loan retrieved successfully.</response>
        /// <response code="404">Loan not found.</response>
        /// <returns>Returns the loan if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(LoanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id)
        {
            var result = await _mediator.Send(new GetLoanByIdQuery(id));

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var loan = result.Value;

            return Ok(loan);
        }

        /// <summary>
        /// Creates a new loan
        /// </summary>
        /// <param name="loanRequest">An object containing the loan's data.</param>
        /// <response code="201">Loan created successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <returns>The newly created loan.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoanResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] CreateLoanCommand loanRequest,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loanRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var loan = result.Value;

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

        /// <summary>
        /// Updates a loan.
        /// </summary>
        /// <param name="id">The unique identifier of the loan.</param>
        /// <param name="loanRequest">An object containing the updated loan data.</param>
        /// <response code="204">Loan updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">Loan not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateLoanCommand loanRequest,
            CancellationToken cancellationToken)
        {
            loanRequest.Id = id;
            var result = await _mediator.Send(loanRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates a loan status.
        /// </summary>
        /// <param name="id">The unique identifier of the loan.</param>
        /// <param name="loanRequest">An object containing the updated loan status.</param>
        /// <response code="204">Loan updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">Loan not found.</response>
        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Status(
            Guid id,
            [FromBody] UpdateLoanStatusCommand loanRequest,
            CancellationToken cancellationToken)
        {
            loanRequest.Id = id;
            var result = await _mediator.Send(loanRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}
