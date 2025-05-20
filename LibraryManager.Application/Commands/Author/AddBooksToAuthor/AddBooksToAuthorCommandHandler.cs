namespace LibraryManager.Application.Commands.Author.AddBooksToAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AddBooksToAuthorCommandHandler
        : ICommandHandler<AddBooksToAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        public AddBooksToAuthorCommandHandler(IAuthorRepository repository, IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<Result> Handle(AddBooksToAuthorCommand request, CancellationToken ct)
        {
            if (request.BookIds == null || !request.BookIds.Any())
            {
                return Result.Failure(Error.NullValue);
            }

            var author = await _repository.GetByIdAsync(request.Id, ct);

            if (author == null)
            {
                return Result.Failure(DomainErrors.Author.NotFound(request.Id));
            }

            var books = await _bookRepository.GetByIdAsync(request.BookIds);

            var validationResult = Validate(books, request.BookIds);

            if (validationResult.IsFailure)
            {
                return Result.Failure(validationResult.Error);
            }

            author.AddBook(books);

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }

        private static Result Validate(IList<Book> existingBooks, IList<Guid> bookIds)
        {
            if (existingBooks == null || !existingBooks.Any())
            {
                return Result.Failure(DomainErrors.Book.NotFoundList(bookIds));
            }

            var existingIds = existingBooks
                .Select(x => x.Id)
                .ToHashSet();

            var booksNotFound = bookIds
                .Where(id => !existingIds.Contains(id))
                .ToList();

            if (booksNotFound.Count != 0)
            {
                return Result.Failure(DomainErrors.Book.NotFoundList(booksNotFound));
            }

            return Result.Success();
        }
    }
}
