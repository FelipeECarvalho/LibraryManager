namespace Library.Core.Repositories
{
    using Library.Core.Entities;

    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        void Add(User user);
        void Update(User user);
    }
}
