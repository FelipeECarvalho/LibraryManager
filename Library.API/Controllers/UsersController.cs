using Library.Application.InputModels.Users;
using Library.Core.Entities;
using Library.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/users")]
    public class UsersController(IUserService _service) : ControllerBase
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
        public async Task<IActionResult> Post([FromBody] UserCreateInputModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Document = model.Document,
                Email = model.Email
            };

            await _service.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserUpdateInputModel model)
        {
            var user = await _service.GetByIdAsync(id);
            user.Update(model.Name, model.Email);

            await _service.UpdateAsync(user);
            return NoContent();
        }
    }
}
