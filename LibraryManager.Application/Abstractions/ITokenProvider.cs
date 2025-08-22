namespace LibraryManager.Application.Abstractions
{
    using LibraryManager.Core.Entities;

    public interface ITokenProvider
    {
        string GenerateRefreshToken();

        Task<string> GenerateTokenAsync(User user);
    }
}
