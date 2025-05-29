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
            var book = await _bookRepository.GetByIdAsync(request.Id, ct);

            if (book == null)
            {
                return Result.Failure(DomainErrors.Book.NotFound(request.Id));
            }

            var result = book.UpdateStock(request.StockNumber);

            if (result.IsFailure) 
            {
                return result;
            }

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
