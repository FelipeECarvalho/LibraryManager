namespace LibraryManager.Application.Queries.Book.GetBooks
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetBooksQueryHandler
        : IQueryHandler<GetBooksQuery, IList<BookResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<IList<BookResponse>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(
                request.LibraryId,
                request.pageSize,
                request.pageNumber,
                request.Title,
                cancellationToken);

            var bookResponse = books?
                .Select(BookResponse.FromEntity)?
                .ToList();

            return bookResponse;
        }
    }
}
