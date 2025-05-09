using Library.Core.Entities;

namespace Library.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        void Add(User user);
        void Update(User user);
    }
}
