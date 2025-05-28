namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync(CancellationToken ct = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsDocumentUnique(string document, CancellationToken ct);
        Task<bool> IsEmailUnique(string email, CancellationToken ct);

        void Add(User user);
        void Update(User user);
    }
}
