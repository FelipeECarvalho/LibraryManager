namespace LibraryManager.Application.Commands.Author.CreateAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading.Tasks;

    internal sealed class CreateAuthorCommandHandler 
        : ICommandHandler<CreateAuthorCommand, AuthorResponse>
    {
        private readonly IAuthorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorCommandHandler(IAuthorRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthorResponse>> Handle(CreateAuthorCommand command, CancellationToken ct)
        {
            var validationResult = Validate(command);

            if (validationResult.IsFailure) 
            {
                return Result.Failure<AuthorResponse>(validationResult.Error);
            }   

            var author = new Author(command.Name, command.Description);

            _repository.Add(author);

            await _unitOfWork.SaveChangesAsync(ct);

            return AuthorResponse.FromEntity(author);
        }

        private static Result Validate(CreateAuthorCommand command)
        {
            if (command == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (string.IsNullOrWhiteSpace(command.Name?.FirstName))
            {
                return Result.Failure(DomainErrors.Name.FirstNameRequired);
            }

            if (command.Name.FirstName.Length > 100 || command.Name.FirstName.Length < 2)
            {
                return Result.Failure(DomainErrors.Name.FirstNameLengthError);
            }

            if (string.IsNullOrWhiteSpace(command.Name?.LastName))
            {
                return Result.Failure(DomainErrors.Name.LastNameRequired);
            }

            if (command.Name.LastName.Length > 100 || command.Name.LastName.Length < 2)
            {
                return Result.Failure(DomainErrors.Name.LastNameLengthError);
            }

            if (command.Description?.Length > 256)
            {
                return Result.Failure(DomainErrors.Author.DescriptionTooLong);
            }

            return Result.Success();
        }
    }
}
