namespace spacexinterop.api._Common.Utility.Mapper;

public partial class Mapper
{
    private MappingProfile<TSource, TDestination> GetProfileOrThrow<TSource, TDestination>()
        where TSource : class
        where TDestination : class
    {
        if (_profiles.TryGetValue((typeof(TSource), typeof(TDestination)), out object? profileObj)
            && profileObj is MappingProfile<TSource, TDestination> profile)
        {
            return profile;
        }

        string errorMessage = $"No mapping profile could be found for {typeof(TSource).Name} to {typeof(TDestination).Namespace}. Please make sure that the profile has been registered in {nameof(MappingConfig)}";
        logger.LogError(errorMessage);
        throw new InvalidOperationException(errorMessage);
    }
}
