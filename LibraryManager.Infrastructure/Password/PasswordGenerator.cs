namespace LibraryManager.Infrastructure.Password
{
    using LibraryManager.Core.Abstractions;
    using System.Security.Cryptography;

    internal sealed class PasswordGenerator : IPasswordGenerator
    {
        private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        public string Generate(int length, int numberOfNonAlphanumericCharacters = 2)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentException(null, nameof(length));
            }

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(null, nameof(numberOfNonAlphanumericCharacters));
            }

            using var rng = RandomNumberGenerator.Create();

            var byteBuffer = new byte[length];

            rng.GetBytes(byteBuffer);

            var count = 0;
            var characterBuffer = new char[length];

            for (var iter = 0; iter < length; iter++)
            {
                var i = byteBuffer[iter] % 87;

                if (i < 10)
                {
                    characterBuffer[iter] = (char)('0' + i);
                }
                else if (i < 36)
                {
                    characterBuffer[iter] = (char)('A' + i - 10);
                }
                else if (i < 62)
                {
                    characterBuffer[iter] = (char)('a' + i - 36);
                }
                else
                {
                    characterBuffer[iter] = Punctuations[i - 62];
                    count++;
                }
            }

            if (count >= numberOfNonAlphanumericCharacters)
            {
                return new string(characterBuffer);
            }

            int j;
            var rand = new Random();

            for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
            {
                int k;
                do
                {
                    k = rand.Next(0, length);
                }
                while (!char.IsLetterOrDigit(characterBuffer[k]));

                characterBuffer[k] = Punctuations[rand.Next(0, Punctuations.Length)];
            }

            return new string(characterBuffer);
        }
    }
}
