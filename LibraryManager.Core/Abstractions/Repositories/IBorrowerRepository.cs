namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IBorrowerRepository
    {
        Task<IList<Borrower>> GetAllAsync(Guid libraryId, int limit = 100, int offset = 1, CancellationToken cancellationToken = default);
        Task<Borrower> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsDocumentUnique(string document, Guid libraryId, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUnique(string email, Guid libraryId, CancellationToken cancellationToken = default);

        void Add(Borrower borrower);
    }
}
