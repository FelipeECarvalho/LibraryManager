namespace LibraryManager.Core.Interfaces
{
    using LibraryManager.Core.Entities;

    public interface ITokenProvider
    {
        string GenerateToken(User user);
    }
}
