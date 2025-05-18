namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using AutoMapper;
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Commands.Author.Add;
    using LibraryManager.Application.InputModels.Authors;
    using LibraryManager.Application.Queries.Author.GetAll;
    using LibraryManager.Application.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorsController(AuthorService _service, IMapper _mapper, IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.QueryAsync(new GetAuthorsQuery(), ct);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await _service.GetByIdAsync(id);

            if (author is null)
                return NotFound();

            var dto = _mapper.Map<AuthorResponse>(author);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] AuthorCreateInputModel model,
            CancellationToken ct)
        {
            var command = new AddAuthorCommand(model.Name, model.Description);

            var result = await _mediator.DispatchAsync(command, ct);

            if (result.IsFailure)
            {
                return 
            }

            return CreatedAtAction(nameof(GetById), new { id = author.Id });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var author = await _service.GetByIdAsync(id);

            //if (author is null)
            //    return NotFound();

            //await _service.DeleteAsync(author);
            await Task.Delay(10);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AuthorUpdateInputModel model)
        {
            var authorResult = await _service.GetByIdAsync(id);

            if (authorResult is null)
                return NotFound();

            var author = authorResult.Value;
            author.Update(model.Name, model.Description);

            await _service.UpdateAsync(author);
            return NoContent();
        }

        [HttpPut("{id:guid}/add-books")]
        public async Task<IActionResult> AddBook(Guid id, [FromBody] AuthorAddBookInputModel model)
        {
            //var authorResult = await _service.GetByIdAsync(id);

            //if (authorResult is null)
            //    return NotFound();

            //await _service.AddBookAsync(author, model.BookIds);
            await Task.Delay(10);
            return NoContent();
        }
    }
}
