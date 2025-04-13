using Library.Application.InputModels.Authors;
using Library.Core.Entities;
using Library.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/authors")]
    public class AuthorsController(IAuthorService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorCreateInputModel model)
        {
            var author = new Author
            {
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Description = model.Description,
                Name = model.Name
            };

            await _service.CreateAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorUpdateInputModel model)
        {
            var author = await _service.GetByIdAsync(id);
            author.Update(model.Name, model.Description);

            await _service.UpdateAsync(author);
            return NoContent();
        }
    }
}
