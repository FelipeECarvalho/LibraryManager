using Library.Core.Entities;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        public Task<Loan> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Loan>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task ILoanService.CreateAsync(Loan loan)
        {
            throw new NotImplementedException();
        }

        Task ILoanService.UpdateAsync(Loan loan)
        {
            throw new NotImplementedException();
        }

        Task ILoanService.ReturnAsync(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
