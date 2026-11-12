using System.Security.Claims;
using System.Text;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Dtx.Security.Identity.Authentication;

internal sealed class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
{
    public string GenerateAccessToken(User user)
    {
        JwtOptions jwt = options.Value;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress ?? string.Empty),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(jwt.ExpirationInMinutes),
            Issuer = jwt.Issuer,
            Audience = jwt.Audience,
            SigningCredentials = credentials,
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }
}
