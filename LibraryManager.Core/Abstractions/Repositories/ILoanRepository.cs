namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Loan>> GetAllAsync(Guid libraryId, int limit = 100, int offset = 1, Guid? BorrowerId = null, CancellationToken cancellationToken = default);

        void Add(Loan loan);
    }
}
