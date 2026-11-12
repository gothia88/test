using Dtx.Security.Api.Infrastructure;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.SharedKernel;
using Dtx.Security.Authorization.Features.AddMembership;
using Dtx.Security.Authorization.Features.CheckPermission;
using Dtx.Security.Authorization.Features.CreateApplicationGroup;
using Dtx.Security.Authorization.Features.DefineApplicationEntity;
using Dtx.Security.Authorization.Features.GrantPermission;
using Dtx.Security.Authorization.Features.RegisterApplication;

namespace Dtx.Security.Api.Endpoints.Authorization;

public static class AuthorizationEndpoints
{
    public static IEndpointRouteBuilder MapAuthorizationEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/authorization").RequireAuthorization();

        group.MapPost("applications", async (
            RegisterApplicationCommand command,
            ICommandHandler<RegisterApplicationCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Authorization");

        group.MapPost("entities", async (
            DefineApplicationEntityCommand command,
            ICommandHandler<DefineApplicationEntityCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Authorization");

        group.MapPost("groups", async (
            CreateApplicationGroupCommand command,
            ICommandHandler<CreateApplicationGroupCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Authorization");

        group.MapPost("permissions", async (
            GrantPermissionCommand command,
            ICommandHandler<GrantPermissionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Authorization");

        group.MapPost("memberships", async (
            AddMembershipCommand command,
            ICommandHandler<AddMembershipCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Authorization");

        group.MapPost("check-permission", async (
            CheckPermissionCommand command,
            ICommandHandler<CheckPermissionCommand, CheckPermissionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<CheckPermissionResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Authorization");

        return app;
    }
}
