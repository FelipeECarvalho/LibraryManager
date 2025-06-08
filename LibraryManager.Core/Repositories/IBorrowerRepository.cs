namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities.Users;

    public interface IBorrowerRepository
    {
        Task<IList<Borrower>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken cancellationToken = default);
        Task<Borrower> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsDocumentUnique(string document, CancellationToken cancellationToken);
        Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);

        void Add(Borrower borrower);
        void Update(Borrower borrower);
    }
}
