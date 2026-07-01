using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Configuration;

namespace Mouts.DeveloperTest.InfraStructure.Authentication
{
    public sealed class JwtTokenGenerator(
    IOptions<JwtOptions> options) : ITokenGenerator
    {
        private readonly JwtOptions _options = options.Value;

        public string Generate(User user)
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.Value.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),

            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
