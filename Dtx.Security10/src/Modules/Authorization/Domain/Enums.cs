namespace Dtx.Security.Authorization.Domain;

public enum AccessType
{
    Special = 0,
    Registered = 1,
    Public = 2,
}

public enum ApplicationEntityType
{
    Controller = 0,
    Action = 1,
    Page = 2,
    ApiController = 3,
    ApiAction = 4,
}

public enum RequestType
{
    Get = 0,
    Post = 1,
    Put = 2,
    Delete = 3,
}
