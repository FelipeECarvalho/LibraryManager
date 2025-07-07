namespace LibraryManager.Tests
{
    using LibraryManager.Infrastructure.Password;
    using Xunit;

    public class PasswordHasherTests
    {
        private readonly PasswordHasher _passwordHasher;

        public PasswordHasherTests()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void HashPassword_ShouldReturnDifferentHash_ForDifferentPasswords()
        {
            var hash1 = _passwordHasher.ComputeHash("123456");
            var hash2 = _passwordHasher.ComputeHash("abcdef");

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
        {
            var password = "123123123";
            var hash = _passwordHasher.ComputeHash(password);

            var result = _passwordHasher.Verify(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForIncorrectPassword()
        {
            var password = "mypassword";
            var hash = _passwordHasher.ComputeHash(password);

            var result = _passwordHasher.Verify("wrongpassword", hash);

            Assert.False(result);
        }
    }
}
