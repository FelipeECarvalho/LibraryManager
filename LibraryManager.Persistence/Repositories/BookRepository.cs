namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces.Repositories;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class BookRepository
        : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Book>> GetAllAsync(int limit = 100, int offset = 1, string title = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Books
                .AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                query = query
                    .Where(x => EF.Functions.Like(x.Title, $"%{title}%"));
            }

            return await query
                .Include(x => x.Author)
                .OrderBy(x => x.CreateDate)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(x => x.Author)
                .Include(x => x.Loans)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IList<Book>> GetByIdAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsIsbnUnique(string isbn, CancellationToken cancellationToken = default)
        {
            return !await _context.Books.AnyAsync(x => x.Isbn == isbn, cancellationToken);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }
    }
}
