namespace LibraryManager.Persistence.Repositories
{
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    internal sealed class RefreshTokenRepository
        : IRefreshTokenRepository
    {
        private readonly LibraryDbContext _context;

        public RefreshTokenRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void Add(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
        }

        public async Task<RefreshToken> GetByIdAsync(Guid id)
        {
            return await _context.RefreshTokens
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
