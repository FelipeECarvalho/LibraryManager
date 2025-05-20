namespace LibraryManager.Application.Commands.Book.UpdateBookStock
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateBookStockCommandHandler
        : ICommandHandler<UpdateBookStockCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookStockCommandHandler(
            IUnitOfWork unitOfWork,
            IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }


        public async Task<Result> Handle(UpdateBookStockCommand request, CancellationToken ct)
        {
            var validationResult = Validate(request);

            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var book = await _bookRepository.GetByIdAsync(request.Id, ct);

            if (book == null)
            {
                return Result.Failure(DomainErrors.Book.NotFound(request.Id));
            }

            book.UpdateStock(request.StockNumber);

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }

        public static Result Validate(UpdateBookStockCommand request)
        {
            if (request == null)
            {
                return Result.Failure(Error.NullValue);
            }

            if (request.StockNumber < 0)
            {
                return Result.Failure(DomainErrors.Book.StockNumberInvalid);
            }

            return Result.Success();
        }
    }
}
