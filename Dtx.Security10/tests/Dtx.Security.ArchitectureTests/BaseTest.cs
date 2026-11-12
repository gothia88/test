using System.Reflection;

namespace Dtx.Security.ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly SharedKernelAssembly =
        typeof(Dtx.Security.SharedKernel.Error).Assembly;

    protected static readonly Assembly ApplicationAssembly =
        typeof(Dtx.Security.Application.DependencyInjection).Assembly;

    protected static readonly Assembly IdentityAssembly =
        typeof(Dtx.Security.Identity.IdentityModuleMarker).Assembly;

    protected static readonly Assembly AuthorizationAssembly =
        typeof(Dtx.Security.Authorization.AuthorizationModuleMarker).Assembly;

    protected static readonly Assembly OrdersAssembly =
        typeof(Dtx.Orders.OrdersModuleMarker).Assembly;
}
