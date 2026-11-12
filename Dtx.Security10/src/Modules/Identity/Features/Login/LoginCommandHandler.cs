using System.Security.Cryptography;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Authentication;
using Dtx.Security.Identity.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dtx.Security.Identity.Features.Login;

internal sealed class LoginCommandHandler(
    IIdentityDbContext context,
    IPasswordHasher<User> passwordHasher,
    IJwtTokenGenerator tokenGenerator,
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtOptions> options)
    : ICommandHandler<LoginCommand, AccessTokensResponse>
{
    public async Task<Result<AccessTokensResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Username == command.Username, cancellationToken);

        if (user is null)
        {
            return Result.Failure<AccessTokensResponse>(UserErrors.InvalidCredentials());
        }

        if (!user.IsActive)
        {
            return Result.Failure<AccessTokensResponse>(UserErrors.InactiveAccount());
        }

        PasswordVerificationResult verification =
            passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

        if (verification == PasswordVerificationResult.Failed)
        {
            return Result.Failure<AccessTokensResponse>(UserErrors.InvalidCredentials());
        }

        string accessToken = tokenGenerator.GenerateAccessToken(user);
        string refreshToken = GenerateRefreshToken();

        context.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            CreatedOnUtc = dateTimeProvider.UtcNow,
            ExpiresOnUtc = dateTimeProvider.UtcNow.AddDays(options.Value.RefreshTokenExpirationInDays),
        });

        context.LoginLogs.Add(new LoginLog
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            LoginDateTime = dateTimeProvider.UtcNow,
        });

        await context.SaveChangesAsync(cancellationToken);

        return new AccessTokensResponse(accessToken, refreshToken);
    }

    private static string GenerateRefreshToken()
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(bytes);
    }
}
