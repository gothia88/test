using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Authentication;

namespace Dtx.Security.Identity.Features.Login;

public sealed record LoginCommand(string Username, string Password) : ICommand<AccessTokensResponse>;
