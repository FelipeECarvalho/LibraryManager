namespace LibraryManager.Application.Queries.Author.GetAuthors
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Interfaces.Repositories;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.Common;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal sealed class GetAuthorsQueryHandler
        : IQueryHandler<GetAuthorsQuery, IList<AuthorResponse>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Result<IList<AuthorResponse>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAllAsync(
                request.PageSize,
                request.PageNumber,
                cancellationToken);

            var response = authors?
                .Select(AuthorResponse.FromEntity)?
                .ToList();

            return response;
        }
    }
}
