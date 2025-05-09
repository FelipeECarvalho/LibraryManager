using Library.Core.Entities;
using Library.Infrastructure;

namespace Library.Persistence.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected readonly LibraryDbContext _context;

        protected BaseRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public virtual async Task CreateAsync(T model)
        {
            _context.Set<T>().Add(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T model)
        {
            _context.Set<T>().Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
