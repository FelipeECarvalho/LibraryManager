using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context)
            : base(context)
        {
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
