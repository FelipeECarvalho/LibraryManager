namespace LibraryManager.Application.Commands.Author.CreateAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces.Repositories;
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

        public async Task<Result<AuthorResponse>> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = new Author(command.Name, command.Description);

            _repository.Add(author);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return AuthorResponse.FromEntity(author);
        }
    }
}
