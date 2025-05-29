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
            var validationResult = await Validate(request, ct);

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

        private async Task<Result> Validate(CreateBookCommand request, CancellationToken ct)
        {
            var isIsbnUnique = await _bookRepository.IsIsbnUnique(request.Isbn, ct);

            if (!isIsbnUnique)
            {
                return Result.Failure<BookResponse>(DomainErrors.Book.IsbnAlreadyExists);
            }

            return Result.Success();
        }
    }
}
