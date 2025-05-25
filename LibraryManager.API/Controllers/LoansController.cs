﻿namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Loan.CreateLoan;
    using LibraryManager.Application.Commands.Loan.UpdateLoan;
    using LibraryManager.Application.Commands.Loan.UpdateLoanStatus;
    using LibraryManager.Application.Queries.Loan.GetLoanById;
    using LibraryManager.Application.Queries.Loan.GetLoans;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoansController(IMediator _mediator) : ApiController
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
                return HandleFailure(result);
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
                return HandleFailure(result);
            }

            return NoContent();
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> Status(Guid id,
            [FromBody] UpdateLoanStatusCommand command,
            CancellationToken ct)
        {
            command.Id = id;
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}
