﻿namespace LibraryManager.Application.Queries.Book.GetBookById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetBookByIdQueryHandler
        : IQueryHandler<GetBookByIdQuery, BookResponse>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

            if (book == null)
            {
                return Result.Failure<BookResponse>(
                    DomainErrors.Book.NotFound(request.Id));
            }

            return BookResponse.FromEntity(book);
        }
    }
}
