namespace LibraryManager.Application.Commands.Book.CreateBook
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Book;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateBookCommandHandler 
        : ICommandHandler<CreateBookCommand, BookResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(
            IAuthorRepository repository,
            IUnitOfWork unitOfWork,
            IBookRepository bookRepository)
        {
            _authorRepository = repository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<Result<BookResponse>> Handle(CreateBookCommand request, CancellationToken ct)
        {
            var validationResult = Validate(request);

            if (validationResult.IsFailure)
            {
                return Result.Failure<BookResponse>(validationResult.Error);
            }

            var author = await _authorRepository.GetByIdAsync(request.AuthorId, ct);

            if (author == null)
            {
                return Result.Failure<BookResponse>(DomainErrors.Author.NotFound(request.AuthorId));
            }

            var book = new Book(
                request.Title.Trim(),
                request.Description?.Trim(),
                request.PublicationDate,
                request.Isbn.Trim(),
                request.StockNumber,
                request.AuthorId);

            _bookRepository.Add(book);

            await _unitOfWork.SaveChangesAsync(ct);

            return BookResponse.FromEntity(book);
        }

        private static Result Validate(CreateBookCommand request)
        {
            if (request == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return Result.Failure(DomainErrors.Book.TitleRequired);
            }

            if (request.Title.Length > 100)
            {
                return Result.Failure(DomainErrors.Book.TitleTooLong);
            }

            if (string.IsNullOrWhiteSpace(request.Isbn))
            {
                return Result.Failure(DomainErrors.Book.IsbnRequired);
            }

            if (request.Isbn.Length > 100 || request.Isbn.Length < 2)
            {
                return Result.Failure(DomainErrors.Book.IsbnTooLong);
            }

            if (request.PublicationDate == default)
            {
                return Result.Failure(DomainErrors.Book.PublicationDateRequired);
            }

            if (request.AuthorId == default)
            {
                return Result.Failure(DomainErrors.Author.NotFound(request.AuthorId));
            }

            return Result.Success();
        }
    }
}
