﻿namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
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

        public async Task<Result<IList<LoanResponse>>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync(
                request.LibraryId,
                request.PageSize,
                request.PageNumber,
                request.BorrowerId,
                cancellationToken);

            var loansResponse = loans?
                .Select(LoanResponse.FromEntity)?
                .ToList();

            return loansResponse;
        }
    }
}
