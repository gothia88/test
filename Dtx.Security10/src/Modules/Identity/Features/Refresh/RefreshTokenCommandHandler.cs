using System.Security.Cryptography;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Authentication;
using Dtx.Security.Identity.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dtx.Security.Identity.Features.Refresh;

internal sealed class RefreshTokenCommandHandler(
    IIdentityDbContext context,
    IJwtTokenGenerator tokenGenerator,
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtOptions> options)
    : ICommandHandler<RefreshTokenCommand, AccessTokensResponse>
{
    public async Task<Result<AccessTokensResponse>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens
            .Include(rt => rt.User)
            .SingleOrDefaultAsync(rt => rt.Token == command.RefreshToken, cancellationToken);

        if (refreshToken is null ||
            refreshToken.IsRevoked ||
            refreshToken.ExpiresOnUtc < dateTimeProvider.UtcNow ||
            refreshToken.User is null)
        {
            return Result.Failure<AccessTokensResponse>(UserErrors.InvalidRefreshToken());
        }

        // Token rotation: mark the current token as used so a stolen token can only be used once.
        refreshToken.RevokedOnUtc = dateTimeProvider.UtcNow;
        refreshToken.UsedOnUtc = dateTimeProvider.UtcNow;

        string newRefreshToken = GenerateRefreshToken();

        context.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = refreshToken.UserId,
            Token = newRefreshToken,
            CreatedOnUtc = dateTimeProvider.UtcNow,
            ExpiresOnUtc = dateTimeProvider.UtcNow.AddDays(options.Value.RefreshTokenExpirationInDays),
        });

        string accessToken = tokenGenerator.GenerateAccessToken(refreshToken.User);

        await context.SaveChangesAsync(cancellationToken);

        return new AccessTokensResponse(accessToken, newRefreshToken);
    }

    private static string GenerateRefreshToken()
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(bytes);
    }
}
