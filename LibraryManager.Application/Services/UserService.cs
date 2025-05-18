namespace LibraryManager.Application.Services
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;

    public sealed class UserService(IUserRepository _repository, IUnitOfWork _unitOfWork)
    {
        public async Task<IList<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _repository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _repository.Update(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            user.SetDeleted();
            _repository.Update(user);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
