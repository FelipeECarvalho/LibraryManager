namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id);
        Task<IList<Loan>> GetAllAsync();

        void Add(Loan loan);
        void Update(Loan loan);
    }
}
