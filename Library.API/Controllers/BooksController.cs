namespace Library.API.Controllers
{
    using Asp.Versioning;
    using AutoMapper;
    using Library.Application.DTOs;
    using Library.Application.InputModels.Books;
    using Library.Application.Services;
    using Library.Core.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController(BookService _bookService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();

            if (books is null || books.Count == 0)
                return NoContent();

            var dto = _mapper.Map<IList<BookDto>>(books);

            return Ok(dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book is null)
                return NotFound();

            var dto = _mapper.Map<BookDto>(book);

            return Ok(dto);
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var books = await _bookService.GetByTitleAsync(title);

            if (books is null || books.Count == 0)
                return NoContent();

            var dto = _mapper.Map<IList<BookDto>>(books);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookCreateInputModel model)
        {
            var book = _mapper.Map<Book>(model);

            await _bookService.CreateAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book is null)
                return NotFound();

            await _bookService.DeleteAsync(book);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BookUpdateInputModel model)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book is null)
                return NotFound();

            book.Update(model.Title, model.Description, model.PublicationDate);

            await _bookService.UpdateAsync(book);
            return NoContent();
        }

        [HttpPut("{id:Guid}/update-stock/{stockNumber:int}")]
        public async Task<IActionResult> Put(Guid id, int stockNumber)
        {
            if (stockNumber < 0)
                return BadRequest("Stock number cannot be a negative number.");

            var book = await _bookService.GetByIdAsync(id);

            if (book is null)
                return BadRequest();

            book.UpdateStock(stockNumber);

            await _bookService.UpdateAsync(book);
            return NoContent();
        }
    }
}