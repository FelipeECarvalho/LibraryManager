namespace LibraryManager.Application.Interfaces
{
    using LibraryManager.Core.Entities;

    public interface ITokenProvider
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
