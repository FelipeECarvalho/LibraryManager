namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal sealed class LibraryRepository
        : ILibraryRepository
    {
        private readonly LibraryDbContext _context;

        public LibraryRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Library> GetById(Guid id, CancellationToken cancellationToken = default) 
        {
            return await _context.Libraries
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
