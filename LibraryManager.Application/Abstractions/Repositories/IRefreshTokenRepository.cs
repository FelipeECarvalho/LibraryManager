namespace LibraryManager.Application.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;
    using System.Threading.Tasks;

    public interface IRefreshTokenRepository
    {
        void Add(RefreshToken refreshToken);

        Task<RefreshToken> GetByTokenAsync(string token);
    }
}