using Library.Core.Entities;
using Library.Core.Repositories;

namespace Library.Application.Services
{
    public sealed class LoanService(
        ILoanRepository _repository,
        IBookRepository _bookRepository,
        IUserRepository _userRepository,
        IUnitOfWork _unitOfWork)
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
            _repository.Add(loan);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Loan loan)
        {
            _repository.Update(loan);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReturnAsync(Loan loan)
        {
            loan.Return();
            _repository.Update(loan);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ValidateCreate(Loan loan)
        {
            var book = await _bookRepository.GetByIdAsync(loan.BookId)
                ?? throw new ArgumentException("Book not found");

            var user = await _userRepository.GetByIdAsync(loan.UserId)
                ?? throw new ArgumentException("User not found");

            var loans = await GetByBookAsync(loan.BookId);

            if (loans.Count(x => !x.IsReturned) >= book.StockNumber)
                throw new Exception("Book is without stock");
        }
    }
}
