namespace LibraryManager.Application.Queries.Author.GetAuthors
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
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

        public async Task<Result<IList<AuthorResponse>>> Handle(GetAuthorsQuery request, CancellationToken ct)
        {
            var authors = await _authorRepository.GetAllAsync(request.ToFilter(), ct);

            var response = authors?
                .Select(AuthorResponse.FromEntity)?
                .ToList();

            return response;
        }
    }
}
