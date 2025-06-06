namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetLoansQueryHandler
        : IQueryHandler<GetLoansQuery, IList<LoanResponse>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoansQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<Result<IList<LoanResponse>>> Handle(GetLoansQuery request, CancellationToken ct)
        {
            var loans = await _loanRepository.GetAllAsync(
                request.Limit,
                request.Offset,
                request.BorrowerId,
                ct);

            var loansResponse = loans?
                .Select(LoanResponse.FromEntity)?
                .ToList();

            return loansResponse;
        }
    }
}
