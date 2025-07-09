namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Loan>> GetAllAsync(Guid libraryId, int pageSize = 100, int pageNumber = 1, Guid? BorrowerId = null, CancellationToken cancellationToken = default);
        Task ProcessOverdueAsync();
        Task ProcessCanceledAsync();

        void Add(Loan loan);
    }
}
