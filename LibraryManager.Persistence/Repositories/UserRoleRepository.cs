namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UserRoleRepository : IUserRoleRepository
    {
        private readonly LibraryDbContext _context;

        public UserRoleRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IList<UserRole>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
