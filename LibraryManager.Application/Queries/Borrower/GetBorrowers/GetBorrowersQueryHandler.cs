namespace LibraryManager.Application.Queries.Borrower.GetBorrowers
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
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

        public async Task<Result<IList<BorrowerResponse>>> Handle(GetBorrowersQuery request, CancellationToken cancellationToken)
        {
            var borrowers = await _borrowerRepository.GetAllAsync(
                request.LibraryId,
                request.PageSize,
                request.PageNumber,
                cancellationToken);

            var borrowersResponse = borrowers?
                .Select(BorrowerResponse.FromEntity)?
                .ToList();

            return borrowersResponse;
        }
    }
}
