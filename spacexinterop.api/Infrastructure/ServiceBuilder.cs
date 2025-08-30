using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api._Common.Utility.Validators;
using spacexinterop.api._Common.Utility.Factories;
using spacexinterop.api._Common.Utility.Clients;
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
        
        services.AddScoped<ISpaceXClient, SpaceXClient>();

        //#region Services
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISpaceXService, SpaceXService>();

        //#endregion

        return services;
    }
}