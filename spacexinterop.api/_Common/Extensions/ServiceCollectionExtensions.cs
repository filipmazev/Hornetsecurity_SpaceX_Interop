namespace spacexinterop.api._Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static TConfig ConfigureModel<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        TConfig config = new();
        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }
}