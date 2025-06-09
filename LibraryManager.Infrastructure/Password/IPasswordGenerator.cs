namespace LibraryManager.Infrastructure.Password
{
    public interface IPasswordGenerator
    {
        string Generate(int length, int numberOfNonAlphanumericCharacters = 2);
    }
}