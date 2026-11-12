using Dtx.Security.Api.Infrastructure;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.SharedKernel;
using Dtx.Security.Identity.Features.ChangePassword;
using Dtx.Security.Identity.Features.GetUserById;
using Dtx.Security.Identity.Features.Login;
using Dtx.Security.Identity.Features.Refresh;
using Dtx.Security.Identity.Features.Register;
using Dtx.Security.Identity.Authentication;

namespace Dtx.Security.Api.Endpoints.Identity;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/identity");

        group.MapPost("register", async (
            RegisterUserCommand command,
            ICommandHandler<RegisterUserCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Identity");

        group.MapPost("login", async (
            LoginCommand command,
            ICommandHandler<LoginCommand, AccessTokensResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<AccessTokensResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Identity");

        group.MapPost("refresh", async (
            RefreshTokenCommand command,
            ICommandHandler<RefreshTokenCommand, AccessTokensResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<AccessTokensResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Identity");

        group.MapPost("change-password", async (
            ChangePasswordCommand command,
            ICommandHandler<ChangePasswordCommand> handler,
            CancellationToken cancellationToken) =>
        {
            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        }).RequireAuthorization().WithTags("Identity");

        group.MapGet("users/{userId:guid}", async (
            Guid userId,
            IQueryHandler<GetUserByIdQuery, UserResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<UserResponse> result = await handler.Handle(new GetUserByIdQuery(userId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).RequireAuthorization().WithTags("Identity");

        return app;
    }
}
