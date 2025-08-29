public static class ServiceCollectionExtensions
{
    public static TConfig ConfigureModel<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        var config = new TConfig();
        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }

    public static TConfig ConfigureModel<TConfig, TConfigImplementation>(this IServiceCollection services, IConfiguration configuration)
        where TConfig : class
        where TConfigImplementation : class, TConfig, new()
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        var config = new TConfigImplementation();
        configuration.Bind(config);
        services.AddSingleton<TConfig>(x => config);
        return config;
    }

    public static TConfig ConfigureModel<TConfig>(
        this IServiceCollection services,
        IConfiguration configuration,
        TConfig config) where TConfig : class
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        if (config == null) throw new ArgumentNullException(nameof(config));

        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }

    public static TConfig ConfigureModel<TConfig>(
        this IServiceCollection services,
        IConfiguration configuration,
        Func<TConfig> modelProvider) where TConfig : class
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        if (modelProvider == null) throw new ArgumentNullException(nameof(modelProvider));

        var config = modelProvider();
        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }
}