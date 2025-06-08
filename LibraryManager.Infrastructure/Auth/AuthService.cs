namespace LibraryManager.Infrastructure.Auth
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Interfaces;
    using LibraryManager.Infrastructure.Options;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public sealed class AuthService
        : IAuthService
    {
        private readonly JwtInfoOptions _options;

        public AuthService(IOptions<JwtInfoOptions> options)
        {
            if (options is null)
            {
                throw new ArgumentException(Error.NullValue);
            }

            _options = options.Value;
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
                DateTime.UtcNow.AddDays(_options.Expires),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
