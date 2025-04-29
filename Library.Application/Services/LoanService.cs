using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class LoanService(ILoanRepository _repository, IBookService _bookService, IUserService _userService) : ILoanService
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
            var book = _bookService.GetByIdAsync(loan.BookId);

            if (book == null)
                throw new Exception("Book not found");

            var user = _userService.GetByIdAsync(loan.UserId);

            if (user == null)
                throw new Exception("User not found");

            if (loan.EndDate < DateTime.Now)
                throw new Exception("The loan end date cannot be smaller than today date");

            return _repository.CreateAsync(loan);
        }

        public Task UpdateAsync(Loan loan)
        {
            if (loan.EndDate < DateTime.Now)
                throw new Exception("The loan end date cannot be smaller than today date");

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
