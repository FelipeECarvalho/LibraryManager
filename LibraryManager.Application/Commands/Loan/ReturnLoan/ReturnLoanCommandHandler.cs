namespace LibraryManager.Application.Commands.Loan.ReturnLoan
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class ReturnLoanCommandHandler
        : ICommandHandler<ReturnLoanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoanRepository _loanRepository;

        public ReturnLoanCommandHandler(
            IUnitOfWork unitOfWork,
            ILoanRepository loanRepository)
        {
            _unitOfWork = unitOfWork;
            _loanRepository = loanRepository;
        }

        public async Task<Result> Handle(ReturnLoanCommand request, CancellationToken ct)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id, ct);

            if (loan == null)
            {
                return Result.Failure(DomainErrors.Loan.NotFound(request.Id));
            }

            if (!loan.Status.IsWithUser())
            {
                return Result.Failure(DomainErrors.Loan.CannotReturnWhenNotBorrowed);
            }

            loan.Return();
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
