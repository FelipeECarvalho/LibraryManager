namespace LibraryManager.Application.Commands.Author.UpdateAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateAuthorCommandHandler
        : ICommandHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken ct)
        {
            Validate(request);

            var author = await _authorRepository.GetByIdAsync(request.Id, ct);

            if (author == null)
            {
                return Result.Failure(DomainErrors.Author.NotFound(request.Id));
            }

            author.Update(request.Name, request.Description);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }

        private static Result Validate(UpdateAuthorCommand command)
        {
            if (command == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (string.IsNullOrEmpty(command.Name?.FirstName))
            {
                return Result.Failure(DomainErrors.Name.FirstNameRequired);
            }

            if (command.Name.FirstName.Length > 100 || command.Name.FirstName.Length < 2)
            {
                return Result.Failure(DomainErrors.Name.FirstNameLengthError);
            }

            if (string.IsNullOrEmpty(command.Name?.LastName))
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
