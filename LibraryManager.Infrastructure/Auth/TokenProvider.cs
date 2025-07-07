namespace LibraryManager.Infrastructure.Auth
{
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Infrastructure;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.JsonWebTokens;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;

    public sealed class TokenProvider
        : ITokenProvider
    {
        private readonly JwtInfoOptions _options;
        private readonly IUserRoleRepository _userRoleRepository;

        public TokenProvider(
            IOptions<JwtInfoOptions> options,
            IUserRoleRepository userRoleRepository)
        {
            if (options is null)
            {
                throw new ArgumentException(Error.NullValue);
            }

            _options = options.Value;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var userRoles = await _userRoleRepository.GetByUserAsync(user.Id);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email.ToString()),
                new("library_id", user.LibraryId.ToString())
            };

            claims.AddRange(
                userRoles.Select(x => new Claim(ClaimTypes.Role, x.Role.RoleType.ToString())));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_options.Expires),
                SigningCredentials = credentials,
                Issuer = _options.Issuer,
                Audience = _options.Audience
            };

            var handler = new JsonWebTokenHandler();

            return handler.CreateToken(tokenDescriptor);
        }
    }
}
