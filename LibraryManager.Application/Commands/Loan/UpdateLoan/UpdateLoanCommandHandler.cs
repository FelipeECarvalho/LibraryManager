namespace LibraryManager.Application.Commands.Loan.UpdateLoan
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateLoanCommandHandler
        : ICommandHandler<UpdateLoanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoanRepository _loanRepository;

        public UpdateLoanCommandHandler(
            IUnitOfWork unitOfWork,
            ILoanRepository loanRepository)
        {
            _unitOfWork = unitOfWork;
            _loanRepository = loanRepository;
        }

        public async Task<Result> Handle(UpdateLoanCommand request, CancellationToken ct)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id, ct);

            if (loan == null)
            {
                return Result.Failure(DomainErrors.Loan.NotFound(request.Id));
            }

            var validationResult = Validate(loan, request);

            if (validationResult.IsFailure)
            {
                return Result.Failure(validationResult.Error);
            }

            loan.Update(request.EndDate);

            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }

        private static Result Validate(Core.Entities.Loan loan, UpdateLoanCommand request)
        {
            if (loan.StartDate >= request.EndDate)
            {
                return Result.Failure(DomainErrors.Loan.InvalidStartDate);
            }

            return Result.Success();
        }
    }
}
