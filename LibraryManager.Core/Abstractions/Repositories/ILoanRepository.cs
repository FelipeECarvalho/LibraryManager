namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Loan>> GetAllAsync(Guid libraryId, int pageSize = 100, int pageNumber = 1, Guid? BorrowerId = null, CancellationToken cancellationToken = default);
        Task<IList<Loan>> GetByStatusAsync(LoanStatus loanStatus);

        Task ProcessOverdueAsync();
        Task ProcessCanceledAsync();

        void Add(Loan loan);
    }
}
