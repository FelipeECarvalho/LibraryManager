namespace LibraryManager.Application.Queries.Borrower.GetBorrowers
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetBorrowersQueryHandler
        : IQueryHandler<GetBorrowersQuery, IList<BorrowerResponse>>
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public GetBorrowersQueryHandler(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public async Task<Result<IList<BorrowerResponse>>> Handle(GetBorrowersQuery request, CancellationToken ct)
        {
            var borrowers = await _borrowerRepository.GetAllAsync(request.Limit, request.Offset, ct);

            var borrowersResponse = borrowers?
                .Select(BorrowerResponse.FromEntity)?
                .ToList();

            return borrowersResponse;
        }
    }
}
