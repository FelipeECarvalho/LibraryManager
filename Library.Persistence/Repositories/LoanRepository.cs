using Library.Core.Entities;
using Library.Core.Repositories;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
    public sealed class LoanRepository : BaseRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context)
            : base(context)
        {
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
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Loan>> GetByBookAsync(int bookId)
        {
            return await _context.Loans
                .AsNoTracking()
                .Where(x  => x.BookId == bookId)
                .ToListAsync();
        }
    }
}
