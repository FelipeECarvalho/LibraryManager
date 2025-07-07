namespace LibraryManager.Infrastructure.Auth
{
    using LibraryManager.Core.Entities;

    public interface ITokenProvider
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
