namespace LibraryManager.Application.Queries.Loan.GetLoanById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetLoanByIdQueryHandler
        : IQueryHandler<GetLoanByIdQuery, LoanResponse>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoanByIdQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<Result<LoanResponse>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);

            if (loan is null)
            {
                return Result.Failure<LoanResponse>(
                    DomainErrors.Loan.NotFound(request.Id));
            }

            return LoanResponse.FromEntity(loan);
        }
    }
}
