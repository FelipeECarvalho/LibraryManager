namespace LibraryManager.Infrastructure.Password
{
    using LibraryManager.Application.Abstractions;
    using LibraryManager.Infrastructure.Constants;
    using System;
    using System.Security.Cryptography;

    public sealed class PasswordHasher : IPasswordHasher
    {
        public string ComputeHash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(PasswordHasherConstants.SaltSize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                PasswordHasherConstants.Iterations,
                PasswordHasherConstants.Algorithm,
                PasswordHasherConstants.HashSize);

            return $"{PasswordHasherConstants.Iterations}.{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";
        }

        public bool Verify(string password, string passwordHash)
        {
            try
            {
                var parts = passwordHash.Split('.');

                if (parts.Length != 3)
                {
                    return false;
                }

                var iterations = int.Parse(parts[0]);
                var salt = Convert.FromHexString(parts[1]);
                var hash = Convert.FromHexString(parts[2]);

                var inputHash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    PasswordHasherConstants.Algorithm,
                    hash.Length
                );

                return CryptographicOperations.FixedTimeEquals(hash, inputHash);
            }
            catch
            {
                return false;
            }
        }
    }
}