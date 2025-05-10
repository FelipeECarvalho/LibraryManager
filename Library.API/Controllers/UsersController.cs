namespace Library.API.Controllers
{
    using Asp.Versioning;
    using AutoMapper;
    using Library.Application.DTOs;
    using Library.Application.InputModels.Users;
    using Library.Core.Entities;
    using Library.Core.ValueObjects;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController(dynamic _service, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();

            if (users is null)
                return NoContent();

            var dto = _mapper.Map<IList<UserDto>>(users);

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            var dto = _mapper.Map<UserDto>(user);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateInputModel model)
        {
            var user = _mapper.Map<User>(model);

            await _service.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            await _service.DeleteAsync(user);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserUpdateInputModel model)
        {
            var user = await _service.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            user.Update(model.Name, model.Address);

            await _service.UpdateAsync(user);
            return NoContent();
        }
    }
}
