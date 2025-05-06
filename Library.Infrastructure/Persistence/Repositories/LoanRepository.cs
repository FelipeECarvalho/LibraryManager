using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class LoanRepository(LibraryDbContext _context) : ILoanRepository
    {
        public async Task CreateAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(x => x.Book)
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<Loan>> GetByBookAsync(int bookId)
        {
            return await _context.Loans
                .AsNoTracking()
                .Where(x  => x.BookId == bookId)
                .ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _context.Loans
                .Include(x => x.Book)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
