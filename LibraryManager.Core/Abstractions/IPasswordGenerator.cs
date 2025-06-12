namespace LibraryManager.Core.Abstractions
{
    public interface IPasswordGenerator
    {
        string Generate(int length, int numberOfNonAlphanumericCharacters = 2);
    }
}