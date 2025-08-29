using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Utility.Validators;
using spacexinterop.api._Common.Utility.Factories;
using spacexinterop.api.Services.Interfaces;
using spacexinterop.api.Services;

namespace spacexinterop.api.Infrastructure;

public static class ServiceBuilder
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<Microsoft.AspNetCore.Identity.ILookupProtectorKeyRing, LookupProtectorKeyRing>();
        services.AddSingleton<Microsoft.AspNetCore.Identity.ILookupProtector, LookupProtector>();

        services.AddScoped<IResultFactory, ResultFactory>();
        services.AddScoped<IValidators, Validators>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}