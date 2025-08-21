namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
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

        public async Task<IList<Book>> GetAllAsync(Guid libraryId, int pageSize = 100, int pageNumber = 1, string title = null, CancellationToken cancellationToken = default)
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
                .Where(x => x.LibraryId == libraryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

        public async Task<bool> IsIsbnUnique(string isbn, Guid libraryId, CancellationToken cancellationToken = default)
        {
            return !await _context.Books.AnyAsync(x => x.Isbn == isbn && x.LibraryId == libraryId, cancellationToken);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }
    }
}
