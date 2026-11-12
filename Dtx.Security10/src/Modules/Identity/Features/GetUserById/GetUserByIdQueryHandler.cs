using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Identity.Features.GetUserById;

internal sealed class GetUserByIdQueryHandler(IIdentityDbContext context)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = await context.Users
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse(u.Id, u.Username, u.FullName, u.EmailAddress, u.Type, u.IsActive))
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
        }

        return user;
    }
}
