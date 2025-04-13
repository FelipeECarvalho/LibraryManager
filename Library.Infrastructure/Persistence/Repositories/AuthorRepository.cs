using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository(LibraryDbContext _context) : IAuthorRepository
    {
        public async Task<IList<Author>> GetAllAsync()
        {
            return await _context.Authors
                .Include(x => x.Books)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
