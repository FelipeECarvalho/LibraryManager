using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context)
            : base(context)
        {
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
