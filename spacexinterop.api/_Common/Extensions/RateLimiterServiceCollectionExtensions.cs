using spacexinterop.api._Common._Configs;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;

namespace spacexinterop.api._Common.Extensions;

public static class RateLimiterServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        configuration.GetSection("RateLimiter");

        services.Configure<RateLimiterConfig>(configuration.GetSection("RateLimiter"));

        services.AddRateLimiter(options =>
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            RateLimiterConfig rateLimiterConfig = serviceProvider.GetRequiredService<IOptions<RateLimiterConfig>>().Value;

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("fixed", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = rateLimiterConfig.PermitLimit,
                        Window = TimeSpan.FromSeconds(rateLimiterConfig.WindowSeconds)
                    }
                )
            );
        });

        return services;
    }
}