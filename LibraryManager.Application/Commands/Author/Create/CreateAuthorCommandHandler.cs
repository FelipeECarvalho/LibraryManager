namespace LibraryManager.Application.Commands.Author.Create
{
    using LibraryManager.Application.Queries.Author.GetAll;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using MediatR;
    using System.Threading.Tasks;

    internal sealed class CreateAuthorCommandHandler 
        : IRequestHandler<CreateAuthorCommand, Result<AuthorResponse>>
    {
        private readonly IAuthorRepository _repository;

        public CreateAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<AuthorResponse>> Handle(CreateAuthorCommand request, CancellationToken ct)
        {
            var author = new Author(request.Name, request.Description);

            _repository.Add(author);

            return AuthorResponse.FromEntity(author);
        }
    }
}
