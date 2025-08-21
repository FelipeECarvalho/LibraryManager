namespace LibraryManager.Application.Queries.Author.GetAuthorById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetAuthorByIdQueryHandler
        : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Result<AuthorResponse>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (author is null)
            {
                return Result.Failure<AuthorResponse>(
                    DomainErrors.Author.NotFound(request.Id));
            }

            return AuthorResponse.FromEntity(author);
        }
    }
}
