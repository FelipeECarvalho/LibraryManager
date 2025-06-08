namespace LibraryManager.Infrastructure.Auth
{
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class AuthService 
        : IAuthService
    {
        private readonly JwtInfoOptions _options;

        public AuthService(IOptions<JwtInfoOptions> options)
        {
            _options = options.Value;
        }

        public string ComputeHash(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(passwordBytes);

            var builder = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            var rnd = new Random();
            var res = new StringBuilder();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }

        public string GenerateToken(string email, string role)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(_options.Expires),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
