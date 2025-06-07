namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities.Users;

    public interface IBorrowerRepository
    {
        Task<IList<Borrower>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken ct = default);
        Task<Borrower> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsDocumentUnique(string document, CancellationToken ct);
        Task<bool> IsEmailUnique(string email, CancellationToken ct);

        void Add(Borrower borrower);
        void Update(Borrower borrower);
    }
}
