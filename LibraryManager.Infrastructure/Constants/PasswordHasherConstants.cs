namespace LibraryManager.Infrastructure.Constants
{
    using System.Security.Cryptography;

    public static class PasswordHasherConstants
    {
        public readonly static int SaltSize = 16;
        public readonly static int HashSize = 32;
        public readonly static int Iterations = 350000;
        public readonly static HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    }
}
