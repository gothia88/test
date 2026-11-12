using Dtx.Security.Identity.Domain;

namespace Dtx.Security.Identity.Abstractions;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
}
