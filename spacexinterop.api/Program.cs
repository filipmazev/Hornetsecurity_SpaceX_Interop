using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using spacexinterop.api._Common;
using spacexinterop.api._Common._Configs;
using spacexinterop.api._Common.Extensions;
using spacexinterop.api._Common.Utility.Clients;
using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data;
using spacexinterop.api.Data.Models;
using spacexinterop.api.Infrastructure;
using System.Text.Json.Serialization;
using Azure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configure Builder

#region Core

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddOpenApi();

builder.Services.AddDependencies();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
else
if (builder.Environment.IsProduction())
{
    string keyVaultUrl = builder.Configuration.GetSection("AzureKeyVault:VaultUri").Value!;

    if (!string.IsNullOrEmpty(keyVaultUrl))
    {
        SecretClient client = new(new Uri(keyVaultUrl), new DefaultAzureCredential());

        Pageable<SecretProperties> secrets = client.GetPropertiesOfSecrets();
        foreach (SecretProperties secret in secrets)
        {
            Response<KeyVaultSecret>? secretValue = client.GetSecret(secret.Name);
            builder.Configuration[secret.Name] = secretValue.Value.Value;
        }
    }
}

builder.Services.AddMemoryCache();

builder.Services.Configure<EncryptionKeyConfig>(builder.Configuration.GetSection("EncryptionKeys"));

#region Space X API Interop

builder.Services.Configure<SpaceXApiConfig>(builder.Configuration.GetSection("ExternalApis:SpaceXApi"));

builder.Services.AddHttpClient<ISpaceXClient, SpaceXClient>((sp, client) =>
{
    SpaceXApiConfig options = sp.GetRequiredService<IOptions<SpaceXApiConfig>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
});

#endregion

#endregion

#region Database Context Setup

builder.Services
    .AddDbContext<MainContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("MainConnection"))
        .LogTo(Console.WriteLine));

#endregion

#region CORS Policy Setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        string[]? allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
        if (allowedOrigins is not null && allowedOrigins.Any())
        {
            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
        else
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    });
});
#endregion

#region Authentication & Identity

builder.Services.AddAuthorization();

builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequiredLength = Constants.MinPasswordLength;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;

        options.User.RequireUniqueEmail = true;

        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Constants.UserLockoutTimespanInMinutes);
        options.Lockout.MaxFailedAccessAttempts = Constants.MaxFailedAccessAttempts;

        options.Stores.ProtectPersonalData = true;
    })
    .AddEntityFrameworkStores<MainContext>()
    .AddDefaultTokenProviders()
    .AddPersonalDataProtection<LookupProtector, LookupProtectorKeyRing>();

builder.Services
    .ConfigureApplicationCookie(options =>
    {
        options.Cookie.Name = "spacexinterop_auth";
        options.Cookie.HttpOnly = true;                                                 // XSS protection (disable JS access)
        options.ExpireTimeSpan = TimeSpan.FromHours(Constants.SessionDurationInHours);  // Session duration
        options.SlidingExpiration = true;                                               // Extend session on activity
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.Cookie.SameSite = builder.Environment.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;

        options.Cookie.Path = "/";
    });

#endregion

#region Rate Limiter Setup

builder.Services.ConfigureModel<RateLimiterConfig>(builder.Configuration.GetSection("RateLimiter"));
builder.Services.AddConfiguredRateLimiter(builder.Configuration);

#endregion

#endregion

WebApplication app = builder.Build();

#region Configure App

#region Service Scope

using IServiceScope scope = app.Services.CreateScope();

IMappingConfig mappingConfig = app.Services.GetRequiredService<IMappingConfig>();
mappingConfig.RegisterMappings();

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Hornet Security | SpaceX Interop API Documentation");
        options.WithTheme(ScalarTheme.BluePlanet);
        options.WithSidebar();
    });

    app.ApplyMigrations();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowedOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.MapControllers();

#endregion

app.Run();