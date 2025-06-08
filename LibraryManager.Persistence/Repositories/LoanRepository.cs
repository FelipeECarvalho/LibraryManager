namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces.Repositories;
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

        public async Task<IList<Loan>> GetAllAsync(int limit = 100, int offset = 1, Guid? BorrowerId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Loans
                .AsNoTracking();

            if (BorrowerId.HasValue)
            {
                query = query
                    .Where(x => x.BorrowerId == BorrowerId);
            }

            return await query
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Loan>> GetByBorrowerAsync(Guid borrowerId, CancellationToken cancellationToken = default)
        {
            return await _context.Loans
                .Where(x => x.Borrower.Id == borrowerId)
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
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

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
        }
    }
}
