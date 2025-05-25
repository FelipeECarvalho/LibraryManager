namespace LibraryManager.Application.Commands.Loan.CreateLoan
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateLoanCommandHandler
        : ICommandHandler<CreateLoanCommand, LoanResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLoanCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ILoanRepository loanRepository,
            IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Result<LoanResponse>> Handle(CreateLoanCommand request, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, ct);

            if (user == null)
            {
                return Result.Failure<LoanResponse>(DomainErrors.User.NotFound(request.UserId));
            }

            var book = await _bookRepository.GetByIdAsync(request.BookId, ct);

            if (book == null)
            {
                return Result.Failure<LoanResponse>(DomainErrors.Book.NotFound(request.BookId));
            }

            if (!book.IsAvaliable())
            {
                return Result.Failure<LoanResponse>(DomainErrors.Book.NotAvaliableForLoan);
            }

            if (!user.CanLoan(book))
            {
                return Result.Failure<LoanResponse>(DomainErrors.Loan.BookAlreadyLoaned);
            }

            var loan = new Loan(user.Id, book.Id, request.StartDate, request.EndDate);

            _loanRepository.Add(loan);

            await _unitOfWork.SaveChangesAsync(ct);

            return LoanResponse.FromEntity(loan);
        }
    }
}
