namespace LibraryManager.Infrastructure.Auth
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Interfaces;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.JsonWebTokens;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;

    public sealed class TokenProvider
        : ITokenProvider
    {
        private readonly JwtInfoOptions _options;

        public TokenProvider(IOptions<JwtInfoOptions> options)
        {
            if (options is null)
            {
                throw new ArgumentException(Error.NullValue);
            }

            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.Email, user.Email.ToString()),
                    new(ClaimTypes.Role, user.GetType().Name),
                    new("library_Id", user.LibraryId.ToString())
                ]),
                Expires = DateTime.UtcNow.AddDays(_options.Expires),
                SigningCredentials = credentials,
                Issuer = _options.Issuer,
                Audience = _options.Audience
            };

            var handler = new JsonWebTokenHandler();

            return handler.CreateToken(tokenDescriptor);
        }
    }
}
