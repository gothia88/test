namespace Dtx.Security.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
