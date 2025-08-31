namespace spacexinterop.api._Common.Utility.Mapper.Interfaces;

public interface IMapper
{
    void AddProfile<TSource, TDestination>(MappingProfile<TSource, TDestination> profile)
        where TSource : class
        where TDestination : class;

    TDestination Map<TSource, TDestination>(TSource source)
        where TSource : class
        where TDestination : class;

    void Map<TSource, TDestination>(TSource source, TDestination destination)
        where TSource : class
        where TDestination : class;
}