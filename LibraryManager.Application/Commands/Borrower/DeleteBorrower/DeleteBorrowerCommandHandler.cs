namespace LibraryManager.Application.Commands.Borrower.DeleteBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class DeleteBorrowerCommandHandler
        : ICommandHandler<DeleteBorrowerCommand>
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBorrowerCommandHandler(
            IUnitOfWork unitOfWork,
            IBorrowerRepository borrowerRepository)
        {
            _unitOfWork = unitOfWork;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<Result> Handle(DeleteBorrowerCommand request, CancellationToken cancellationToken)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(request.Id, cancellationToken);

            if (borrower == null)
            {
                return Result.Failure(DomainErrors.Borrower.NotFound(request.Id));
            }

            borrower.SetDeleted();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
