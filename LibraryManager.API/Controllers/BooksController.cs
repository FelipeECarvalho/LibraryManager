namespace LibraryManager.API.Controllers
{
    using LibraryManager.Application.Commands.Book.CreateBook;
    using LibraryManager.Application.Commands.Book.DeleteBook;
    using LibraryManager.Application.Commands.Book.UpdateBook;
    using LibraryManager.Application.Commands.Book.UpdateBookStock;
    using LibraryManager.Application.Queries.Book;
    using LibraryManager.Application.Queries.Book.GetBookById;
    using LibraryManager.Application.Queries.Book.GetBooks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A book
    /// </summary>
    [ApiController]
    [Authorize]
    public class BooksController(IMediator _mediator) : ApiControllerBase
    {
        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <response code="200">Books retrieved successfully.</response>
        /// <returns>A list containing all books.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<BookResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetBooksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves the book with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <response code="200">Book retrieved successfully.</response>
        /// <response code="404">Book not found.</response>
        /// <returns>Returns the book if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="bookRequest">An object containing the book's data.</param>
        /// <response code="201">Book created successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <returns>The newly created book.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] CreateBookCommand bookRequest,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(bookRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var book = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        /// <summary>
        /// Deletes a book by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <response code="204">Book deleted successfully.</response>
        /// <response code="404">Book not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id), cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <param name="authorRequest">An object containing the updated book data.</param>
        /// <response code="204">Book updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">Book not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateBookCommand authorRequest)
        {
            authorRequest.Id = id;

            var result = await _mediator.Send(authorRequest);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates a book stock.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <param name="stockNumber">The updated stock number.</param>
        /// <response code="204">Book updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">Book not found.</response>
        [HttpPut("{id:Guid}/stock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromQuery] int stockNumber,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateBookStockCommand(id, stockNumber), cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return NoContent();
        }
    }
}