namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IList<Loan>> GetAllAsync(int limit = 100, int offset = 1, Guid? BorrowerId = null, CancellationToken ct = default);
        Task<IList<Loan>> GetByBorrowerAsync(Guid borrowerId, CancellationToken ct = default);

        void Add(Loan loan);
        void Update(Loan loan);
    }
}
