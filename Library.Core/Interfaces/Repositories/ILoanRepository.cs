using Library.Core.Entities;

namespace Library.Core.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task<IList<Loan>> GetAllAsync();

        Task CreateAsync(Loan loan);
        Task UpdateAsync(Loan loan);
    }
}
