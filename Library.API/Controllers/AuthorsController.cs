using AutoMapper;
using Library.Application.DTOs;
using Library.Application.InputModels.Authors;
using Library.Core.Entities;
using Library.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/authors")]
    public class AuthorsController(IAuthorService _service, IMapper _mapper) : ControllerBase
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
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
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _service.GetByIdAsync(id);

            if (author is null)
                return NotFound();

            await _service.DeleteAsync(author);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorUpdateInputModel model)
        {
            var author = await _service.GetByIdAsync(id);

            if (author is null)
                return NotFound();

            _mapper.Map(model, author);

            await _service.UpdateAsync(author);
            return NoContent();
        }

        [HttpPut("{id:int}/add-book/{bookId:int}")]
        public async Task<IActionResult> AddBook(int id, int bookId)
        {
            var author = await _service.GetByIdAsync(id);

            if (author is null)
                return NotFound();

            await _service.UpdateAsync(author);
            return NoContent();
        }
    }
}
