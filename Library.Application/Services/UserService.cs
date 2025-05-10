using Library.Core.Entities;
using Library.Core.Repositories;

namespace Library.Application.Services
{
    public sealed class UserService(IUserRepository _repository, IUnitOfWork _unityOfWork)
    {
        public async Task<IList<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _repository.Add(user);
            await _unityOfWork.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _repository.Update(user);

            await _unityOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            user.SetDeleted();
            _repository.Update(user);

            await _unityOfWork.SaveChangesAsync();
        }
    }
}
