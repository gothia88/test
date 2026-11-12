using Dtx.Security.Application.Abstractions.Authentication;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Identity.Features.ChangePassword;

internal sealed class ChangePasswordCommandHandler(
    IIdentityDbContext context,
    IPasswordHasher<User> passwordHasher,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(userContext.UserId));
        }

        PasswordVerificationResult currentVerification =
            passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.CurrentPassword);

        if (currentVerification == PasswordVerificationResult.Failed)
        {
            return Result.Failure(UserErrors.IncorrectCurrentPassword());
        }

        List<string> previousHashes = await context.PasswordHistories
            .Where(h => h.UserId == user.Id)
            .Select(h => h.PasswordHash)
            .ToListAsync(cancellationToken);

        bool reused = previousHashes.Any(h =>
            passwordHasher.VerifyHashedPassword(user, h, command.NewPassword) == PasswordVerificationResult.Success);

        if (reused)
        {
            return Result.Failure(UserErrors.PasswordReused());
        }

        string newHash = passwordHasher.HashPassword(user, command.NewPassword);

        user.PasswordHash = newHash;

        context.PasswordHistories.Add(new UserPasswordHistory
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            PasswordHash = newHash,
            CreatedOnUtc = dateTimeProvider.UtcNow,
        });

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
