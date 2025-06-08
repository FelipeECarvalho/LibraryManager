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
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLoanCommandHandler(
            IUnitOfWork unitOfWork,
            IBorrowerRepository borrowerRepository,
            ILoanRepository loanRepository,
            IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _borrowerRepository = borrowerRepository;
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Result<LoanResponse>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(request.BorrowerId, cancellationToken);

            if (borrower == null)
            {
                return Result.Failure<LoanResponse>(DomainErrors.Borrower.NotFound(request.BorrowerId));
            }

            var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

            if (book == null)
            {
                return Result.Failure<LoanResponse>(DomainErrors.Book.NotFound(request.BookId));
            }

            if (!book.IsAvailable())
            {
                return Result.Failure<LoanResponse>(DomainErrors.Book.NotAvaliableForLoan);
            }

            if (!borrower.CanLoan(book))
            {
                return Result.Failure<LoanResponse>(DomainErrors.Loan.BookAlreadyLoaned);
            }

            var loan = new Loan(borrower.Id, book.Id, request.StartDate, request.EndDate, request.Observation);

            _loanRepository.Add(loan);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return LoanResponse.FromEntity(loan);
        }
    }
}
