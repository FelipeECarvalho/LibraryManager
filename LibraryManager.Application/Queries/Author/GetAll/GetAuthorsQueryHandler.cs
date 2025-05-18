namespace LibraryManager.Application.Queries.Author.GetAll
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal sealed class GetAuthorsQueryHandler(IUnitOfWork _unitOfWork) 
        : IQueryHandler<GetAuthorsQuery, IList<AuthorResponse>>
    {
        public async Task<Result<IList<AuthorResponse>>> HandleAsync(GetAuthorsQuery query, CancellationToken ct)
        {
            var authors = await _unitOfWork.Authors.GetAllAsync(ct);

            var response = authors?
                .Select(AuthorResponse.FromEntity)?
                .ToList();

            return response; 
        }
    }
}
