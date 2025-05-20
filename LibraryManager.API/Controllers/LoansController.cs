namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Loan.CreateLoan;
    using LibraryManager.Application.Commands.Loan.ReturnLoan;
    using LibraryManager.Application.Commands.Loan.UpdateLoan;
    using LibraryManager.Application.Queries.Loan.GetLoanById;
    using LibraryManager.Application.Queries.Loan.GetUserLoans;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoansController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetLoansQuery());

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLoanByIdQuery(id));

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var loan = result.Value;

            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLoanCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var loan = result.Value;

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateLoanCommand command,
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

        [HttpPatch("{id:guid}/return")]
        public async Task<IActionResult> Return(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new ReturnLoanCommand(id), ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
