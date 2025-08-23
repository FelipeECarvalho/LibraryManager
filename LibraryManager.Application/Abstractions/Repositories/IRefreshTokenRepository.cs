namespace LibraryManager.Application.Abstractions.Repositories
{
    using LibraryManager.Application.Models;
    using System.Threading.Tasks;

    public interface IRefreshTokenRepository
    {
        void Add(RefreshToken refreshToken);

        Task<RefreshToken> GetByTokenAsync(string token);
    }
}