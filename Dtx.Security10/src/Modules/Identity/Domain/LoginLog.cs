using Dtx.Security.SharedKernel;

namespace Dtx.Security.Identity.Domain;

public sealed class LoginLog : Entity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string? UserIP { get; set; }

    public DateTime LoginDateTime { get; set; }

    public DateTime? LogoutDateTime { get; set; }
}
