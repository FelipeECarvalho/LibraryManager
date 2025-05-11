namespace Library.API.Controllers
{
    using Asp.Versioning;
    using AutoMapper;
    using Library.Application.DTOs;
    using Library.Application.InputModels.Users;
    using Library.Application.Services;
    using Library.Core.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController(UserService _userService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            if (users is null)
                return NoContent();

            var dto = _mapper.Map<IList<UserDto>>(users);

            return Ok(dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            var dto = _mapper.Map<UserDto>(user);

            return Ok(dto);
        }

        [HttpGet("{id:guid}/loans")]
        public Task<IActionResult> GetLoans(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateInputModel model)
        {
            var user = _mapper.Map<User>(model);

            await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            await _userService.DeleteAsync(user);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserUpdateInputModel model)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            user.Update(model.Name, model.Address);

            await _userService.UpdateAsync(user);
            return NoContent();
        }
    }
}
