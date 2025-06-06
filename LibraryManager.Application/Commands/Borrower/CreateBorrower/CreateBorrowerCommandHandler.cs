namespace LibraryManager.Application.Commands.Borrower.CreateBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Borrower;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateBorrowerCommandHandler
        : ICommandHandler<CreateBorrowerCommand, BorrowerResponse>
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBorrowerCommandHandler(
            IUnitOfWork unitOfWork,
            IBorrowerRepository borrowerRepository)
        {
            _unitOfWork = unitOfWork;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<Result<BorrowerResponse>> Handle(CreateBorrowerCommand request, CancellationToken ct)
        {
            var validationResult = await ValidateAsync(request, ct);

            if (validationResult.IsFailure)
            {
                return Result.Failure<BorrowerResponse>(validationResult.Error);
            }

            var borrower = new Borrower(request.Name, request.Document, request.Email, request.BirthDate, request.Address);

            _borrowerRepository.Add(borrower);

            await _unitOfWork.SaveChangesAsync(ct);

            return BorrowerResponse.FromEntity(borrower);
        }

        private async Task<Result> ValidateAsync(CreateBorrowerCommand request, CancellationToken ct)
        {
            var isEmailUnique = await _borrowerRepository.IsEmailUnique(request.Email, ct);

            if (!isEmailUnique)
            {
                return Result.Failure(DomainErrors.Borrower.EmailAlreadyExists);
            }

            var isDocumentUnique = await _borrowerRepository.IsDocumentUnique(request.Document, ct);

            if (isDocumentUnique)
            {
                return Result.Failure(DomainErrors.Borrower.DocumentAlreadyExists);
            }

            return Result.Success();
        }
    }
}
