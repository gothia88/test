using System.Text;
using Dtx.Orders;
using Dtx.Security.Api.Endpoints.Authorization;
using Dtx.Security.Api.Endpoints.Identity;
using Dtx.Security.Api.Endpoints.Orders;
using Dtx.Security.Api.Infrastructure;
using Dtx.Security.Application.Abstractions.Authentication;
using Dtx.Security.Authorization;
using Dtx.Security.Identity;
using Dtx.Security.SharedKernel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddIdentityModule(builder.Configuration)
    .AddAuthorizationModule(builder.Configuration)
    .AddOrdersModule(builder.Configuration);

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddHealthChecks();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation());

WebApplication app = builder.Build();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityEndpoints();
app.MapAuthorizationEndpoints();
app.MapOrdersEndpoints();

app.MapHealthChecks("health");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Run();

// Required so integration tests can reference the web app via WebApplicationFactory.
// The protected constructor below satisfies the "static holder type" analyzer
// (CA1052) without changing the type's runtime behaviour.
public partial class Program
{
    protected Program()
    {
    }
}
