namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using AutoMapper;
    using LibraryManager.Application.DTOs;
    using LibraryManager.Application.InputModels.Authors;
    using LibraryManager.Application.Services;
    using LibraryManager.Core.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorsController(AuthorService _service, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _service.GetAllAsync();

            if (authors is null)
                return NoContent();

            var dto = _mapper.Map<IList<AuthorDto>>(authors);

            return Ok(dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await _service.GetByIdAsync(id);

            if (author is null)
                return NotFound();

            var dto = _mapper.Map<AuthorDto>(author);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorCreateInputModel model)
        {
            var author = _mapper.Map<Author>(model);

            await _service.CreateAsync(author);

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
