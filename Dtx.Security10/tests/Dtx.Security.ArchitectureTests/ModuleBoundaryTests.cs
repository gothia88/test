using System.Reflection;
using NetArchTest.Rules;
using Shouldly;
using Xunit;

namespace Dtx.Security.ArchitectureTests;

public class ModuleBoundaryTests : BaseTest
{
    [Fact]
    public void SharedKernel_ShouldNotDependOn_AnyOtherProject()
    {
        AssertNoDependency(SharedKernelAssembly,
            "Dtx.Security.Application",
            "Dtx.Security.Identity",
            "Dtx.Security.Authorization",
            "Dtx.Security.Api",
            "Dtx.Orders");
    }

    [Fact]
    public void Application_ShouldNotDependOn_ModulesOrApi()
    {
        AssertNoDependency(ApplicationAssembly,
            "Dtx.Security.Identity",
            "Dtx.Security.Authorization",
            "Dtx.Security.Api",
            "Dtx.Orders");
    }

    [Fact]
    public void IdentityModule_ShouldNotDependOn_OtherModulesOrApi()
    {
        AssertNoDependency(IdentityAssembly,
            "Dtx.Security.Authorization",
            "Dtx.Security.Api",
            "Dtx.Orders");
    }

    [Fact]
    public void AuthorizationModule_ShouldNotDependOn_OtherModulesOrApi()
    {
        AssertNoDependency(AuthorizationAssembly,
            "Dtx.Security.Identity",
            "Dtx.Security.Api",
            "Dtx.Orders");
    }

    [Fact]
    public void OrdersModule_ShouldNotDependOn_OtherModulesOrApi()
    {
        AssertNoDependency(OrdersAssembly,
            "Dtx.Security.Identity",
            "Dtx.Security.Authorization",
            "Dtx.Security.Api");
    }

    private static void AssertNoDependency(Assembly assembly, params string[] forbiddenDependencies)
    {
        foreach (string dependency in forbiddenDependencies)
        {
            TestResult result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn(dependency)
                .GetResult();

            result.IsSuccessful
                .ShouldBeTrue($"Assembly '{assembly.GetName().Name}' must not depend on '{dependency}'.");
        }
    }
}
