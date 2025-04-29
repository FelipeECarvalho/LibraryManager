using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class LoanService(ILoanRepository _repository) : ILoanService
    {
        public Task<Loan> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IList<Loan>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task CreateAsync(Loan loan)
        {
            return _repository.CreateAsync(loan);
        }

        public Task UpdateAsync(Loan loan)
        {
            return _repository.UpdateAsync(loan);
        }

        public Task ReturnAsync(Loan loan)
        {
            loan.IsReturned = true;
            loan.UpdateDate = DateTime.Now;

            return _repository.UpdateAsync(loan);
        }
    }
}
