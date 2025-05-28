namespace LibraryManager.Application.Commands.Loan.UpdateLoanStatus
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Errors;
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

            var validationResult = ValidateStatusTransition(loan.Status, request.Status);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }

        private static Result ValidateStatusTransition(LoanStatus current, LoanStatus requested)
        {
            return requested switch
            {
                LoanStatus.Approved when current != LoanStatus.Requested =>
                    Result.Failure(DomainErrors.Loan.CannotApproveWhenNotRequested),

                LoanStatus.Cancelled when !current.CanBeCancelled() =>
                    Result.Failure(DomainErrors.Loan.CannotCancelInThisStatus),

                LoanStatus.Borrowed when current != LoanStatus.Approved =>
                    Result.Failure(DomainErrors.Loan.CannotBorrowWhenNotApproved),

                LoanStatus.Returned when !current.IsWithUser() =>
                    Result.Failure(DomainErrors.Loan.CannotReturnWhenNotBorrowed),

                _ => Result.Success()
            };
        }
    }
}
