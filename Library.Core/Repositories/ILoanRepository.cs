namespace Library.Core.Repositories
{
    using Library.Core.Entities;

    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id);
        Task<IList<Loan>> GetAllAsync();
        Task<IList<Loan>> GetByBookAsync(Guid bookId);

        void Add(Loan loan);
        void Update(Loan loan);
    }
}
