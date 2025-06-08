namespace LibraryManager.Core.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string email, string role);
    }
}
