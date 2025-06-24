namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Enums;
    using LibraryManager.Core.Extensions;
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

        public async Task<IList<Loan>> GetAllAsync(Guid libraryId, int limit = 100, int offset = 1, Guid? borrowerId = null, CancellationToken cancellationToken = default)
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
                .Skip((offset - 1) * limit)
                .Take(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task ProcessOverdueAsync()
        {
            await _context.Loans
                .Where(x => x.Status == LoanStatus.Borrowed && x.EndDate < DateTime.UtcNow)
                .ExecuteUpdateAsync(x => x.SetProperty(l => l.Status, LoanStatus.Overdue));
        }

        public async Task ProcessCanceledAsync()
        {
            await _context.Loans
                .Where(x => x.Status == LoanStatus.Approved && x.StartDate > DateTime.UtcNow.AddDays(7))
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
