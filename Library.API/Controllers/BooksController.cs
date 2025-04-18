using Library.Application.InputModels.Books;
using Library.Core.Entities;
using Library.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/books")]
    public class BooksController(IBookService _bookService) : ControllerBase
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

        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            return Ok(await _bookService.GetByTitleAsync(title));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookCreateInputModel model)
        {
            var book = new Book
            {
                ISBN = model.ISBN,
                Title = model.Title,
                AuthorId = model.AuthorId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Description = model.Description,
                PublicationDate = model.PublicationDate,
            };

            await _bookService.CreateAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id}, book);
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
            var book = await _bookService.GetByIdAsync(id);
            book.Update(model.Title, model.Description, model.PublicationDate);

            await _bookService.UpdateAsync(book);
            return NoContent();
        }
    }
}