namespace LibraryManager.API.Controllers
{
    using LibraryManager.Application.Commands.Author.CreateAuthor;
    using LibraryManager.Application.Commands.Author.DeleteAuthor;
    using LibraryManager.Application.Commands.Author.UpdateAuthor;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Application.Queries.Author.GetAuthorById;
    using LibraryManager.Application.Queries.Author.GetAuthors;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Hybrid;

    /// <summary>
    /// An Author
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorsController(
        IMediator _mediator,
        HybridCache _hybridCache) : ApiControllerBase
    {
        public static readonly Func<Guid, string> AuthorCacheKey = id => $"author:{id}";

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        /// <response code="200">Authors retrieved successfully.</response>
        /// <returns>A list containing all authors.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<AuthorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAuthorsQuery query,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"authors";

            var result = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async _ => await _mediator.Send(query, cancellationToken),
                cancellationToken: cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves the author with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <response code="200">Author retrieved successfully.</response>
        /// <response code="404">Author not found.</response>
        /// <returns>Returns the author if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetAuthorByIdQuery(id);

            var result = await _hybridCache.GetOrCreateAsync(
                AuthorCacheKey(id),
                async _ => await _mediator.Send(query, cancellationToken),
                cancellationToken: cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Creates a new author
        /// </summary>
        /// <param name="authorRequest">An object containing the author's data.</param>
        /// <response code="201">Author created successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <returns>The newly created author.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] CreateAuthorCommand authorRequest,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(authorRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var author = result.Value;
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        /// <summary>
        /// Deletes an author by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <response code="204">Author deleted successfully.</response>
        /// <response code="404">Author not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(id), cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            await _hybridCache.RemoveAsync(AuthorCacheKey(id), cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Updates an auhtor.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <param name="authorRequest">An object containing the updated author data.</param>
        /// <response code="204">Author updated successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">Author not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            Guid id,
            [FromBody] UpdateAuthorCommand authorRequest,
            CancellationToken cancellationToken)
        {
            authorRequest.Id = id;
            var result = await _mediator.Send(authorRequest, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            await _hybridCache.RemoveAsync(AuthorCacheKey(id), cancellationToken);

            return NoContent();
        }
    }
}
