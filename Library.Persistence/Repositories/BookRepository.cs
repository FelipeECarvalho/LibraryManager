namespace Library.Persistence.Repositories
{
    using Library.Core.Entities;
    using Library.Core.Repositories;
    using Library.Persistence;
    using Microsoft.EntityFrameworkCore;

    public sealed class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
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
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Book>> GetByTitleAsync(string title)
        {
            return await _context.Books
                .Include(x => x.Author)
                .Where(x => EF.Functions.Like(x.Title, $"%{title}%"))
                .AsNoTracking()
                .ToListAsync();
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
