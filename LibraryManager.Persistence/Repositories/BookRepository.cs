namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using LibraryManager.Persistence;
    using Microsoft.EntityFrameworkCore;

    internal sealed class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Book>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .ToListAsync(ct);
        }

        public async Task<Book> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Books
                .Include(x => x.Author)
                .Include(x => x.Loans)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IList<Book>> GetByIdAsync(IList<Guid> ids, CancellationToken ct)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(ct);
        }

        public async Task<IList<Book>> GetByTitleAsync(string title, CancellationToken ct)
        {
            return await _context.Books
                .Include(x => x.Author)
                .Where(x => EF.Functions.Like(x.Title, $"%{title}%"))
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<bool> IsIsbnUnique(string isbn, CancellationToken ct = default)
        {
            return !await _context.Books.AnyAsync(x => x.Isbn == isbn, ct);
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
