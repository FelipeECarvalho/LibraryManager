namespace LibraryManager.Application.Commands.Borrower.CreateBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Borrower;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Interfaces;
    using LibraryManager.Core.Interfaces.Repositories;
    using LibraryManager.Core.ValueObjects;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateBorrowerCommandHandler
        : ICommandHandler<CreateBorrowerCommand, BorrowerResponse>
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordGenerator _passwordGenerator;

        public CreateBorrowerCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IPasswordGenerator passwordGenerator,
            IBorrowerRepository borrowerRepository,
            ILibraryRepository libraryRepository)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _passwordGenerator = passwordGenerator;
            _borrowerRepository = borrowerRepository;
            _libraryRepository = libraryRepository;
        }

        public async Task<Result<BorrowerResponse>> Handle(CreateBorrowerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateAsync(request, cancellationToken);

            if (validationResult.IsFailure)
            {
                return Result.Failure<BorrowerResponse>(validationResult.Error);
            }

            var library = await _libraryRepository.GetById(request.LibraryId, cancellationToken);

            if (library == null)
            {
                return Result.Failure<BorrowerResponse>(DomainErrors.Library.IdNotFound(request.LibraryId));
            }

            var password = string.IsNullOrWhiteSpace(request.Password)
                ? _passwordGenerator.Generate(length: 10)
                : request.Password;

            password = _passwordHasher.ComputeHash(password);

            var borrower = new Core.Entities.Borrower(
                request.Name,
                new Email(request.Email),
                password,
                request.Document,
                request.BirthDate,
                library.Id,
                request.Address);

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

            if (!isDocumentUnique)
            {
                return Result.Failure(DomainErrors.Borrower.DocumentAlreadyExists);
            }

            return Result.Success();
        }
    }
}
