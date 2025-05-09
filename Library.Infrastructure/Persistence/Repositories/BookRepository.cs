using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public sealed class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context)
            : base(context)
        {
        }

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

        public async Task<IList<Book>> GetByTitleAsync(string title)
        {
            return await base._context.Books
                .Include(x => x.Author)
                .Where(x => EF.Functions.Like(x.Title, $"%{title}%"))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
