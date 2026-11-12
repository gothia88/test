using System.Security.Claims;
using Dtx.Security.Application.Abstractions.Authentication;

namespace Dtx.Security.Api.Infrastructure;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId
    {
        get
        {
            ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

            if (user is null)
            {
                return Guid.Empty;
            }

            Claim? claim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");

            return claim is not null && Guid.TryParse(claim.Value, out Guid id) ? id : Guid.Empty;
        }
    }
}
