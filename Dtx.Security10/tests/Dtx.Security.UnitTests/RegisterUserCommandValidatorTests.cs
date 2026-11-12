using Dtx.Security.Identity.Features.Register;
using Shouldly;
using Xunit;

namespace Dtx.Security.UnitTests;

public class RegisterUserCommandValidatorTests
{
    [Fact]
    public void ValidCommand_ShouldPassValidation()
    {
        var validator = new RegisterUserCommandValidator();
        var command = new RegisterUserCommand("john.doe", "P@ssw0rd123", "John Doe", "john@example.com");

        validator.Validate(command).IsValid.ShouldBeTrue();
    }

    [Fact]
    public void ShortPassword_ShouldFailValidation()
    {
        var validator = new RegisterUserCommandValidator();
        var command = new RegisterUserCommand("john.doe", "short", "John Doe");

        validator.Validate(command).IsValid.ShouldBeFalse();
    }

    [Fact]
    public void EmptyUsername_ShouldFailValidation()
    {
        var validator = new RegisterUserCommandValidator();
        var command = new RegisterUserCommand(string.Empty, "P@ssw0rd123", "John Doe");

        validator.Validate(command).IsValid.ShouldBeFalse();
    }
}
