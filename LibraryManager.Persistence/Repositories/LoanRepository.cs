namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class LoanRepository
        : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Loan>> GetAllAsync(Guid libraryId, int pageSize = 100, int pageNumber = 1, Guid? borrowerId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Loans
                .AsNoTracking();

            if (borrowerId.HasValue)
            {
                query = query
                    .Where(x => x.BorrowerId == borrowerId);
            }

            return await query
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                .Where(x => x.Borrower.LibraryId == libraryId)
                .OrderBy(x => x.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Loan>> GetByStatusAsync(LoanStatus loanStatus)
        {
            return await _context.Loans
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Status == loanStatus)
                .ToListAsync();
        }

        public async Task ProcessOverdueAsync()
        {
            await _context.Loans
                .Where(x => x.Status == LoanStatus.Borrowed && x.EndDate < DateTimeOffset.UtcNow)
                .ExecuteUpdateAsync(x => x.SetProperty(l => l.Status, LoanStatus.Overdue));
        }

        public async Task ProcessCanceledAsync()
        {
            await _context.Loans
                .Where(x => x.Status == LoanStatus.Approved && x.StartDate > DateTimeOffset.UtcNow.AddDays(7))
                .ExecuteUpdateAsync(x => x.SetProperty(l => l.Status, LoanStatus.Cancelled));
        }

        public async Task<Loan> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Loans
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
        }
    }
}
