namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
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

        public async Task<IList<Loan>> GetAllAsync(int limit, int offset, Guid? UserId, CancellationToken ct)
        {
            var query = _context.Loans
                .AsNoTracking();

            if (UserId.HasValue)
            {
                query = query
                    .Where(x => x.UserId == UserId);
            }

            return await query
                .Include(x => x.User)
                .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(offset)
                .ToListAsync(ct);
        }

        public async Task<IList<Loan>> GetByUserAsync(Guid userId, CancellationToken ct)
        {
            return await _context.Loans
                .Where(x => x.User.Id == userId)
                .Include(x => x.User)
                .Include(x => x.Book)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Loan> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Loans
                .Include(x => x.User)
                .Include(x => x.Book)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
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
