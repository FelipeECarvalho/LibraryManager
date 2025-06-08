namespace LibraryManager.Application.Commands.Borrower.CreateBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Borrower;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using LibraryManager.Core.ValueObjects;
    using LibraryManager.Infrastructure.Auth;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateBorrowerCommandHandler
        : ICommandHandler<CreateBorrowerCommand, BorrowerResponse>
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateBorrowerCommandHandler(
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IBorrowerRepository borrowerRepository)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<Result<BorrowerResponse>> Handle(CreateBorrowerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateAsync(request, cancellationToken);

            if (validationResult.IsFailure)
            {
                return Result.Failure<BorrowerResponse>(validationResult.Error);
            }

            var password = string.IsNullOrWhiteSpace(request.Password)
                ? _authService.GeneratePassword(length: 10)
                : request.Password;

            password = _authService.ComputeHash(password);

            var borrower = new Core.Entities.Users.Borrower(request.Name, new Email(request.Email), password, request.Document, request.BirthDate, request.Address);

            _borrowerRepository.Add(borrower);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BorrowerResponse.FromEntity(borrower);
        }

        private async Task<Result> ValidateAsync(CreateBorrowerCommand request, CancellationToken cancellationToken)
        {
            var isEmailUnique = await _borrowerRepository.IsEmailUnique(request.Email, cancellationToken);

            if (!isEmailUnique)
            {
                return Result.Failure(DomainErrors.User.EmailAlreadyExists);
            }

            var isDocumentUnique = await _borrowerRepository.IsDocumentUnique(request.Document, cancellationToken);

            if (isDocumentUnique)
            {
                return Result.Failure(DomainErrors.Borrower.DocumentAlreadyExists);
            }

            return Result.Success();
        }
    }
}
