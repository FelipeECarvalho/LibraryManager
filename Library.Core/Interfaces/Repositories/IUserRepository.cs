using Library.Core.Entities;

namespace Library.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        Task CreateAsync(User user);
        Task UpdateAsync(User user);
    }
}
