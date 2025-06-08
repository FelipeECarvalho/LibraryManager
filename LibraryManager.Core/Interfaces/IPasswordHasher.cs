namespace LibraryManager.Core.Interfaces
{
    public interface IPasswordHasher
    {
        string ComputeHash(string password);
        bool Verify(string password, string passwordHash);
    }
}
