namespace LibraryManager.Infrastructure.Auth
{
    public interface IAuthService
    {
        string GeneratePassword(int length);
        string ComputeHash(string password);
        string GenerateToken(string email, string role);
    }
}
