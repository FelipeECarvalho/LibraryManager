namespace LibraryManager.Application.Queries.User.GetUserLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetUserLoansQueryHandler
        : IQueryHandler<GetUserLoansQuery, IList<LoanResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;

        public GetUserLoansQueryHandler(IUserRepository userRepository, ILoanRepository loanRepository)
        {
            _userRepository = userRepository;
            _loanRepository = loanRepository;
        }

        public async Task<Result<IList<LoanResponse>>> Handle(GetUserLoansQuery request, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, ct);

            if (user == null)
            {
                return Result.Failure<IList<LoanResponse>>(DomainErrors.User.NotFound(request.Id));
            }

            var loans = await _loanRepository.GetByUserAsync(user.Id, ct);

            var loansResponse = loans?
                .Select(LoanResponse.FromEntity)?
                .ToList();

            return loansResponse;
        }
    }
}
