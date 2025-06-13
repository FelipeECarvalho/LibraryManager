namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using Microsoft.EntityFrameworkCore;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailLoadRole(string email, Guid libraryId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .SingleOrDefaultAsync(x => x.Email == email && x.LibraryId == libraryId, cancellationToken);
        }
    }
}
