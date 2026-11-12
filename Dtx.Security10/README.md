# Dtx.Security10

A **.NET 10 modular monolith** re-implementation of the legacy `Dtx.Security`
"Triple-A" (Authentication / Authorization / Accounting) service, rebuilt with
modern best practices.

## Architecture

```
src/
  SharedKernel/          Result/Error, ErrorType, ValidationError, Entity, IDomainEvent, IDateTimeProvider
  Application/           shared use-case plumbing: ICommand/IQuery + handlers,
                         ValidationDecorator + LoggingDecorator (Scrutor),
                         domain-event dispatcher, DomainEventsDbContext, PagedList
  Modules/
    Identity/            Users, JWT auth, refresh tokens, password history, IP allow-list
    Authorization/       Client applications, groups, entities, permissions, memberships, check-permission
    Orders/              Orders + order items; create (with a domain event) and query (paged)
  Api/                   host — minimal APIs, composition root, JWT, EF Core 10 + PostgreSQL,
                         Serilog, OpenTelemetry, ProblemDetails, health checks, Scalar (dev)
tests/
  Dtx.Security.ArchitectureTests/   NetArchTest module-boundary enforcement
  Dtx.Security.UnitTests/           Result + validator unit tests
```

Dependencies point inward and **modules do not reference each other**: they
communicate only through `SharedKernel`/`Application` contracts and by `Guid`
foreign keys (e.g. `Membership.UserId` references a user in the Identity module
without a navigation property). The architecture tests enforce this.

## Key patterns

- **Vertical-slice CQRS** (no MediatR): each use case is a `Command`/`Query` +
  `Handler` + FluentValidation `Validator`, auto-registered by Scrutor and wrapped
  by validation + logging decorators.
- **Errors, not exceptions**: handlers return `Result`/`Result<T>` with a typed
  `Error`; endpoints translate via `result.Match(Results.Ok, CustomResults.Problem)`.
- **JWT bearer auth** with **refresh-token rotation**, and **PBKDF2 password
  hashing** via ASP.NET Core Identity's `PasswordHasher<T>`.
- **EF Core 10 + PostgreSQL** with snake_case naming and one schema per module
  (`identity`, `authorization`).
- **Domain events** dispatched after `SaveChangesAsync` (eventual consistency).

## Run

Requires the **.NET 10 SDK** and **PostgreSQL**.

```bash
# 1. create the database (migrations not yet generated; see below)
# 2. run
dotnet run --project src/Api
# 3. open https://localhost:xxxx/scalar/v1 in Development
```

Endpoints (prefixes `api/identity` and `api/authorization`):

- `POST api/identity/register`
- `POST api/identity/login` → `{ accessToken, refreshToken }`
- `POST api/identity/refresh`
- `POST api/identity/change-password` (auth)
- `GET  api/identity/users/{id}` (auth)
- `POST api/authorization/applications|entities|groups|permissions|memberships` (auth)
- `POST api/authorization/check-permission` (auth) → `{ isGranted }`

## Tests

```bash
dotnet test Dtx.Security10.slnx
```

## Notes / next steps

- **Migrations**: create them with
  `dotnet ef migrations add Initial --project src/Modules/Identity --context IdentityDbContext`
  (and the same for `AuthorizationDbContext`), or temporarily use
  `EnsureCreated()`.
- `Jwt:Secret` in `appsettings.json` is a development placeholder — replace it
  and move it to user-secrets/Key Vault in production.
- Integration tests (Testcontainers) are intentionally omitted; add a
  `Dtx.Security.IntegrationTests` project with `WebApplicationFactory` +
  `Testcontainers.PostgreSql` when Docker is available.
- SHA1/MD5 are **not** used here; passwords use PBKDF2 (`PasswordHasher<T>`).
