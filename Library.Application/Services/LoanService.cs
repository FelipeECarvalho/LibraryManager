namespace LibraryManager.Application.Services
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Repositories;

    public sealed class LoanService(
        ILoanRepository _repository,
        IBookRepository _bookRepository,
        IUserRepository _userRepository,
        IUnitOfWork _unitOfWork)
    {
        public async Task<Loan> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IList<Loan>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
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

            if (book.Loans.Count(x => x.LoanStatus == LoanStatus.Borrowed) >= book.StockNumber)
                throw new Exception("Book without stock");
        }
    }
}
