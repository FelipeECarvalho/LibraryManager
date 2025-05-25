﻿namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Author.CreateAuthor;
    using LibraryManager.Application.Commands.Author.DeleteAuthor;
    using LibraryManager.Application.Commands.Author.UpdateAuthor;
    using LibraryManager.Application.Queries.Author.GetAuthorById;
    using LibraryManager.Application.Queries.Author.GetAuthors;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorsController(IMediator _mediator) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAuthorsQuery(), ct);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetAuthorByIdQuery(id);

            var result = await _mediator.Send(query, ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] CreateAuthorCommand command,
            CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var author = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(id), ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateAuthorCommand command,
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
