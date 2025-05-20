namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync(CancellationToken ct);
        Task<User> GetByIdAsync(Guid id, CancellationToken ct);

        void Add(User user);
        void Update(User user);
    }
}
