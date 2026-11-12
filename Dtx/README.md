# Dtx — modern .NET 10 port

A faithful rewrite of the legacy `Dtx` / `Dtx.FileManager` utility libraries
(from `JUST_CMS_ECOMMERCE`) as SDK-style **.NET 10** class libraries.

## Projects

| Project | Assembly | Description |
| --- | --- | --- |
| `src/Dtx` | `Dtx.dll` | Persian (Jalali) calendar, text/number conversion, hashing, RSS model, SMTP mail, logging, IO/string helpers, Kendo grid models. |
| `src/Dtx.FileManager` | `Dtx.FileManager.dll` | File-manager view models and localized resources. |

## What was ported

- `Dtx.Calendar` — Persian/Jalali date math (`PersianDate`, `Convert`, …)
- `Dtx.Text` — Persian digits/number/date text and regular-expression helpers
- `Dtx.Security` — MD5/SHA1 hashing
- `Dtx.Rss` — channel/item model and serialization
- `Dtx.Net.Mail` — SMTP mail (`MailMessage`, `IMailSettings`)
- `Dtx.Net.LogHandler` — logging (see changes below)
- `Dtx.IO`, `Dtx.String`, `Dtx.Guid`, `Dtx.Math`, `Dtx.Random`, `Dtx.Parser`,
  `Dtx.PropertiesMapper`, `Dtx.Globalization`, enums, and the Kendo grid models

## Changes from the .NET Framework original

- **Dropped** `Dtx.Windows.Forms` (WinForms UI components) and `Dtx.Web.HttpContext`
  (`System.Web` resource helper) — Windows/ASP.NET-only, no portable equivalent.
- **Dropped** the `SmartStringResources` T4-generated `*.tt.cs` "strings" layer
  (the underlying `.resx` + strongly-typed `*.Designer.cs` resources are retained).
- **`ApplicationSettings`** reads from a pluggable `ValueProvider` delegate
  (wire up `IConfiguration`) with an environment-variable fallback — replaces
  `System.Configuration.ConfigurationManager`.
- **`IO.File` / `LogHandler`** resolve `~/`-relative paths against
  `AppContext.BaseDirectory` — replaces `HttpContext.Server.MapPath`; log writes
  use a `lock` instead of `HttpApplication.Application.Lock/UnLock`.
- **`LogHandler`** dropped the `System.Web.HttpContext` request enrichment and the
  EF6 `DbEntityValidationException` branch.
- **`Kendo.GridPost`** is now web-framework-agnostic (`GridPost.Parse(getValue)`);
  **`Kendo.HtmlHelpers`** uses `System.Linq.Dynamic.Core` instead of the retired
  `System.Linq.Dynamic`.
- **`IO.Directory.SetFullControlForEveryOne`** is a no-op (was Windows ACL setup).
- **`Security.Hashing`** uses `MD5.Create()`/`SHA1.Create()` (the obsolete
  `*CryptoServiceProvider` types were removed).

## Build

Requires the **.NET 10 SDK**.

```bash
dotnet build Dtx.sln
```

> The repo also compiles on the .NET 8 SDK (the code is pure BCL): temporarily
> set `<TargetFramework>net8.0</TargetFramework>` or build with a newer SDK.

## Notes

- `Dtx/` is deliberately isolated from the parent `Directory.Build.props` /
  `Directory.Packages.props` (the Clean Architecture template's central config)
  via its own shield files.
- SHA1/MD5 remain only for backward-compatible checksum/hash use; use a
  password hashing algorithm (e.g. PBKDF2/Argon2) for new code.
