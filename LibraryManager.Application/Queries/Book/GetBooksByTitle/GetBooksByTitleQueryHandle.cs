namespace LibraryManager.Application.Queries.Book.GetBooksByTitle
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetBooksByTitleQueryHandle
        : IQueryHandler<GetBooksByTitleQuery, IList<BookResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksByTitleQueryHandle(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<IList<BookResponse>>> Handle(GetBooksByTitleQuery request, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                return Result.Failure<IList<BookResponse>>(Error.NullValue);
            }

            var books = await _bookRepository.GetByTitleAsync(request.Title, ct);

            var bookResponse = books?
                .Select(BookResponse.FromEntity)?
                .ToList();

            return bookResponse;
        }
    }
}
