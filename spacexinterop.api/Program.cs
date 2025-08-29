using spacexinterop.api._Common.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using spacexinterop.api._Common._Configs;
using spacexinterop.api.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using spacexinterop.api.Data.Models;
using spacexinterop.api._Common;
using spacexinterop.api.Core;
using spacexinterop.api.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure Builder

#region Core

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddOpenApi();

builder.Services.AddDependencies();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.Configure<EncryptionKeyConfig>(builder.Configuration.GetSection("EncryptionKeys"));

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
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
        if (allowedOrigins != null && allowedOrigins.Any())
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

        if (builder.Environment.IsDevelopment())
        {
            options.Cookie.SameSite = SameSiteMode.None;
        }
        else
        {
            options.Cookie.SameSite = SameSiteMode.Strict;
        }

        options.Cookie.Path = "/";
    });

#endregion

#region Rate Limiter Setup

builder.Services.ConfigureModel<RateLimiterConfig>(builder.Configuration.GetSection("RateLimiter"));
builder.Services.AddConfiguredRateLimiter(builder.Configuration);

#endregion

#endregion

var app = builder.Build();

#region Configure App

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Hornetsecurity | SpaceX Interop API Documentation");
        options.WithTheme(ScalarTheme.BluePlanet);
        options.WithSidebar(true);
    });

    app.ApplyMigrations();
}
else
{
    app.UseExceptionHandler();
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