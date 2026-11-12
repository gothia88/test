using Dtx.Security.SharedKernel;
using Shouldly;
using Xunit;

namespace Dtx.Security.UnitTests;

public class ResultTests
{
    [Fact]
    public void Success_ShouldHaveIsSuccessTrue()
    {
        Result result = Result.Success();

        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }

    [Fact]
    public void Failure_ShouldCarryTheError()
    {
        Error error = Error.NotFound("Test.NotFound", "Not found");

        Result result = Result.Failure(error);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void GenericResult_ShouldConvertImplicitlyFromValue()
    {
        var id = Guid.NewGuid();

        Result<Guid> result = id;

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(id);
    }
}
