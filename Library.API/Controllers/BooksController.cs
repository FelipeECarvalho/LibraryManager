using Library.Application.InputModels.Books;
using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/books")]
    public class BooksController(BookService _bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _bookService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookCreateInputModel model)
        {
            var book = await _bookService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = book.Id});
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookUpdateInputModel model)
        {
            await _bookService.UpdateAsync(id, model);
            return NoContent();
        }
    }
}