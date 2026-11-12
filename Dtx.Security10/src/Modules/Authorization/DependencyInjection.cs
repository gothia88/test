using Dtx.Security.Application;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dtx.Security.Authorization;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<AuthorizationDbContext>(options => options
            .UseNpgsql(connectionString, npgsql => npgsql
                .MigrationsHistoryTable("__EFMigrationsHistory", "authorization"))
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IAuthorizationDbContext>(sp => sp.GetRequiredService<AuthorizationDbContext>());

        services.AddModuleHandlers<AuthorizationModuleMarker>();

        return services;
    }
}
