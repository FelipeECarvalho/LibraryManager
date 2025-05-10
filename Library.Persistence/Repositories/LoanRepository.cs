using Library.Core.Entities;
using Library.Core.Repositories;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public sealed class LoanRepository : ILoanRepository
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

        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _context.Loans
                .Include(x => x.User)
                .Include(x => x.Book)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Loan>> GetByBookAsync(int bookId)
        {
            return await _context.Loans
                .AsNoTracking()
                .Where(x  => x.BookId == bookId)
                .ToListAsync();
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
