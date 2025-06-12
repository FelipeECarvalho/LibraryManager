namespace LibraryManager.Core.Abstractions
{
    using LibraryManager.Core.Entities;

    public interface ITokenProvider
    {
        string GenerateToken(User user);
    }
}
