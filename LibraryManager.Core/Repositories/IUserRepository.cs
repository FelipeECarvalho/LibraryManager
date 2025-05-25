namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync(CancellationToken ct = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<User> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<User> GetByDocumentAsync(string document, CancellationToken ct = default);

        void Add(User user);
        void Update(User user);
    }
}
