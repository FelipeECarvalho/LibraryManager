﻿namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Book.CreateBook;
    using LibraryManager.Application.Commands.Book.DeleteBook;
    using LibraryManager.Application.Commands.Book.UpdateBook;
    using LibraryManager.Application.Commands.Book.UpdateBookStock;
    using LibraryManager.Application.Queries.Book.GetBookById;
    using LibraryManager.Application.Queries.Book.GetBooks;
    using LibraryManager.Application.Queries.Book.GetBooksByTitle;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController(IMediator _mediator) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBooksQuery(), ct);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query, ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetByTitle([FromQuery] string title, CancellationToken ct)
        {
            var query = new GetBooksByTitleQuery(title);
            var result = await _mediator.Send(query, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var book = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id), ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBookCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        [HttpPut("{id:Guid}/stock")]
        public async Task<IActionResult> Put(Guid id, [FromQuery] int stockNumber, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateBookStockCommand(id, stockNumber), ct);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}