namespace LibraryManager.Application.Queries.Borrower.GetBorrowerById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Interfaces.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetBorrowerByIdQueryHandler
        : IQueryHandler<GetBorrowerByIdQuery, BorrowerResponse>
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public GetBorrowerByIdQueryHandler(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public async Task<Result<BorrowerResponse>> Handle(GetBorrowerByIdQuery request, CancellationToken cancellationToken)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(request.Id, cancellationToken);

            if (borrower == null)
            {
                return Result.Failure<BorrowerResponse>(DomainErrors.Borrower.NotFound(request.Id));
            }

            return BorrowerResponse.FromEntity(borrower);
        }
    }
}
