using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Identity.Features.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
