using Dtx.Orders.Abstractions;
using Dtx.Orders.Persistence;
using Dtx.Security.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dtx.Orders;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<OrdersDbContext>(options => options
            .UseNpgsql(connectionString, npgsql => npgsql
                .MigrationsHistoryTable("__EFMigrationsHistory", "orders"))
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IOrdersDbContext>(sp => sp.GetRequiredService<OrdersDbContext>());

        services.AddModuleHandlers<OrdersModuleMarker>();

        return services;
    }
}
