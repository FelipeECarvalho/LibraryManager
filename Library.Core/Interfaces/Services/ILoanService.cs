using Library.Core.Entities;

namespace Library.Core.Interfaces.Services
{
    public interface ILoanService
    {
        Task<Loan> GetByIdAsync(int id);
        Task<IList<Loan>> GetAllAsync(); 

        Task CreateAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task ReturnAsync(Loan loan);
    }
}
