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
            var validationResult = Validate(request);

            if (validationResult.IsFailure)
            {
                return Result.Failure<LoanResponse>(validationResult.Error);
            }

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

            var loan = new Loan(user.Id, book.Id, request.StartDate, request.EndDate);

            _loanRepository.Add(loan);

            await _unitOfWork.SaveChangesAsync(ct);

            return LoanResponse.FromEntity(loan);
        }

        private static Result Validate(CreateLoanCommand request)
        {
            if (request.UserId == Guid.Empty)
            {
                return Result.Failure(DomainErrors.Loan.UserIdRequired);
            }

            if (request.BookId == Guid.Empty)
            {
                return Result.Failure(DomainErrors.Loan.BookIdRequired);
            }

            if (request.StartDate >= request.EndDate)
            {
                return Result.Failure(DomainErrors.Loan.InvalidStartDate);
            }

            if (request.StartDate < DateTimeOffset.UtcNow)
            {
                return Result.Failure(DomainErrors.Loan.StartDateInPast);
            }

            return Result.Success();
        }
    }
}
