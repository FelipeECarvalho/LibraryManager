namespace LibraryManager.Application.Commands.Loan.UpdateLoanStatus
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateLoanStatusCommandHandler
        : ICommandHandler<UpdateLoanStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoanRepository _loanRepository;

        public UpdateLoanStatusCommandHandler(
            IUnitOfWork unitOfWork,
            ILoanRepository loanRepository)
        {
            _unitOfWork = unitOfWork;
            _loanRepository = loanRepository;
        }

        public async Task<Result> Handle(UpdateLoanStatusCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);

            if (loan == null)
            {
                return Result.Failure(DomainErrors.Loan.NotFound(request.Id));
            }

            var result = loan.UpdateStatus(request.Status);

            if (result.IsFailure)
            {
                return result;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
