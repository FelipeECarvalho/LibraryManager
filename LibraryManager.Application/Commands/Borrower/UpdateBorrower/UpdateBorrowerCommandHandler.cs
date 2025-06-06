namespace LibraryManager.Application.Commands.Borrower.UpdateBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateBorrowerCommandHandler
        : ICommandHandler<UpdateBorrowerCommand>
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBorrowerCommandHandler(
            IUnitOfWork unitOfWork,
            IBorrowerRepository borrowerRepository)
        {
            _unitOfWork = unitOfWork;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<Result> Handle(UpdateBorrowerCommand request, CancellationToken ct)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(request.Id, ct);

            if (borrower == null)
            {
                return Result.Failure(DomainErrors.Borrower.NotFound(request.Id));
            }

            borrower.Update(request.Name, request.Address);

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
