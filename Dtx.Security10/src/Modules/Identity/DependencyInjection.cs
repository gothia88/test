using Dtx.Security.Application;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Authentication;
using Dtx.Security.Identity.Domain;
using Dtx.Security.Identity.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dtx.Security.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<IdentityDbContext>(options => options
            .UseNpgsql(connectionString, npgsql => npgsql
                .MigrationsHistoryTable("__EFMigrationsHistory", "identity"))
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IIdentityDbContext>(sp => sp.GetRequiredService<IdentityDbContext>());

        services.AddModuleHandlers<IdentityModuleMarker>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        return services;
    }
}
