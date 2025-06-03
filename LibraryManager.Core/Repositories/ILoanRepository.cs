namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IList<Loan>> GetAllAsync(int limit = 100, int offset = 1, Guid? UserId = null, CancellationToken ct = default);
        Task<IList<Loan>> GetByUserAsync(Guid userId, CancellationToken ct = default);

        void Add(Loan loan);
        void Update(Loan loan);
    }
}
