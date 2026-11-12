using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Authentication;

namespace Dtx.Security.Identity.Features.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokensResponse>;
