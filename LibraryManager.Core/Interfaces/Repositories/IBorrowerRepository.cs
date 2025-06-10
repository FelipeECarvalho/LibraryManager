namespace LibraryManager.Core.Interfaces.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IBorrowerRepository
    {
        Task<IList<Borrower>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken cancellationToken = default);
        Task<Borrower> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsDocumentUnique(string document, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken = default);

        void Add(Borrower borrower);
    }
}
