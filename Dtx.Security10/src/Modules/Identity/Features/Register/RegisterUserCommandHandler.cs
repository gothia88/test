using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Identity.Features.Register;

internal sealed class RegisterUserCommandHandler(
    IIdentityDbContext context,
    IPasswordHasher<User> passwordHasher,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        bool exists = await context.Users
            .AnyAsync(u => u.Username == command.Username, cancellationToken);

        if (exists)
        {
            return Result.Failure<Guid>(UserErrors.UsernameAlreadyExists(command.Username));
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            FullName = command.FullName,
            EmailAddress = command.EmailAddress,
            CellPhoneNumber = command.CellPhoneNumber,
            NationalCode = command.NationalCode,
            Type = UserType.User,
            IsActive = true,
            CreatedOnUtc = dateTimeProvider.UtcNow,
        };

        user.PasswordHash = passwordHasher.HashPassword(user, command.Password);

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
