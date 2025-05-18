namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(x => x.User)
                .Include(x => x.Book)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(Guid id)
        {
            return await _context.Loans
                .Include(x => x.User)
                .Include(x => x.Book)
                .SingleOrDefaultAsync(x => x.Id == id);
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
