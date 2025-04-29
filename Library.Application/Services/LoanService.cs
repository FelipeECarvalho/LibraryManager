using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class LoanService(ILoanRepository _repository, IBookService _bookService, IUserService _userService) : ILoanService
    {
        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IList<Loan>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(Loan loan)
        {
            var book = await _bookService.GetByIdAsync(loan.BookId);

            if (book == null)
                throw new Exception("Book not found");

            var user = await _userService.GetByIdAsync(loan.UserId);

            if (user == null)
                throw new Exception("User not found");

            if (loan.EndDate < DateTime.Now)
                throw new Exception("The loan end date cannot be smaller than today date");

            await _repository.CreateAsync(loan);
        }

        public async Task UpdateAsync(Loan loan)
        {
            if (loan.EndDate < DateTime.Now)
                throw new Exception("The loan end date cannot be smaller than today date");

            await _repository.UpdateAsync(loan);
        }

        public async Task ReturnAsync(Loan loan)
        {
            loan.IsReturned = true;
            loan.UpdateDate = DateTime.Now;

            await _repository.UpdateAsync(loan);
        }
    }
}
