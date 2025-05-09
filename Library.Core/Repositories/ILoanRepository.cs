using Library.Core.Entities;

namespace Library.Core.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task<IList<Loan>> GetAllAsync();
        Task<IList<Loan>> GetByBookAsync(int bookId);

        void Add(Loan loan);
        void Update(Loan loan);
    }
}
