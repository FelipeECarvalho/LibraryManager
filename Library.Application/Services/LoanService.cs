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

        public async Task<IList<Loan>> GetByBookAsync(int bookId)
        {
            return await _repository.GetByBookAsync(bookId);
        }

        public async Task CreateAsync(Loan loan)
        {
            await ValidateCreate(loan);
            await _repository.CreateAsync(loan);
        }

        public async Task UpdateAsync(Loan loan)
        {
            await _repository.UpdateAsync(loan);
        }

        public async Task ReturnAsync(Loan loan)
        {
            loan.Return();
            await _repository.UpdateAsync(loan);
        }

        private async Task ValidateCreate(Loan loan) 
        {
            var book = await _bookService.GetByIdAsync(loan.BookId)
                ?? throw new ArgumentException("Book not found");

            var user = await _userService.GetByIdAsync(loan.UserId)
                ?? throw new ArgumentException("User not found");

            var loans = await GetByBookAsync(loan.BookId);

            if (loans.Count(x => !x.IsReturned) >= book.StockNumber)
                throw new Exception("Book is without stock");
        }
    }
}
