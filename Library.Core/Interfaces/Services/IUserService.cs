using Library.Core.Entities;

namespace Library.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
