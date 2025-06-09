namespace LibraryManager.Core.Interfaces
{
    public interface IPasswordGenerator
    {
        string Generate(int length, int numberOfNonAlphanumericCharacters = 2);
    }
}