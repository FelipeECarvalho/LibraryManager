namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.ValueObjects;
    using Microsoft.EntityFrameworkCore;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email, Guid libraryId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Email == email && x.LibraryId == libraryId, cancellationToken);
        }
    }
}
