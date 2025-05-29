namespace LibraryManager.Application.Commands.Loan.UpdateLoanStatus
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Extensions;
    using LibraryManager.Core.Repositories;
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

        public async Task<Result> Handle(UpdateLoanStatusCommand request, CancellationToken ct)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id, ct);

            if (loan == null)
            {
                return Result.Failure(DomainErrors.Loan.NotFound(request.Id));
            }

            var result = loan.UpdateStatus(request.Status);

            if (result.IsFailure)
            {
                return result;
            }
            
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
