using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class UserService(IUserRepository _repository) : IUserService
    {
        public async Task<IList<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(Book user)
        {
            await _repository.CreateAsync(book);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            user.IsDeleted = true;
            user.UpdateDate = DateTime.Now;

            await _repository.UpdateAsync(user);
        }
    }
}
