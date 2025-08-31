using spacexinterop.api._Common.Utility.Mapper.Interfaces;

namespace spacexinterop.api._Common.Utility.Mapper;

public partial class Mapper(
    ILogger<Mapper> logger
    ) : IMapper
{
    private readonly Dictionary<(Type, Type), object> _profiles = new();

    public void AddProfile<TSource, TDestination>(MappingProfile<TSource, TDestination> profile)
        where TSource : class
        where TDestination : class
    {
        _profiles[(typeof(TSource), typeof(TDestination))] = profile;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
        where TSource : class
        where TDestination : class
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        TDestination destination = (TDestination)Activator.CreateInstance(typeof(TDestination), nonPublic: true)!;

        MappingProfile<TSource, TDestination> profile = GetProfileOrThrow<TSource, TDestination>();
        profile.Apply(source, destination);

        return destination;
    }

    public void Map<TSource, TDestination>(TSource source, TDestination destination)
        where TSource : class
        where TDestination : class
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (destination is null) throw new ArgumentNullException(nameof(destination));

        MappingProfile<TSource, TDestination> profile = GetProfileOrThrow<TSource, TDestination>();
        profile.Apply(source, destination);
    }
}