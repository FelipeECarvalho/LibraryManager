using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class BookRepository(LibraryDbContext _context) : IBookRepository
    {
        public async Task<IList<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(x => x.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
