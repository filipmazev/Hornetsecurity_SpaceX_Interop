using System.Linq.Expressions;
using System.Reflection;

namespace spacexinterop.api._Common.Utility.Mapper;

public class MappingProfile<TSource, TDestination>
    where TSource : class
    where TDestination : class
{
    private readonly List<Action<TSource, TDestination>> _mappings = [];

    public MappingProfile<TSource, TDestination> ForProperty<TSourceProperty, TDestProperty>(
        Expression<Func<TSource, TSourceProperty>> sourceSelector,
        Expression<Func<TDestination, TDestProperty>> destinationSelector
    )
    {
        PropertyInfo sourceProperty = (PropertyInfo)((MemberExpression)sourceSelector.Body).Member;
        PropertyInfo destinationProperty = (PropertyInfo)((MemberExpression)destinationSelector.Body).Member;

        _mappings.Add((source, destination) =>
        {
            object? value = sourceProperty.GetValue(source);
            destination?.GetType().GetProperty(destinationProperty.Name)?.SetValue(destination, value);
        });

        return this;
    }

    public MappingProfile<TSource, TDestination> ForProperty<TSourceProperty, TDestProperty>(
        Expression<Func<TSource, TSourceProperty>> sourceSelector,
        Expression<Func<TDestination, TDestProperty>> destinationSelector,
        Func<TSourceProperty, TDestProperty> castingFunction)
    {
        PropertyInfo sourceProperty = (PropertyInfo)((MemberExpression)sourceSelector.Body).Member;
        PropertyInfo destinationProperty = (PropertyInfo)((MemberExpression)destinationSelector.Body).Member;

        _mappings.Add((source, destination) =>
        {
            TSourceProperty? sourceValue = (TSourceProperty?)sourceProperty.GetValue(source);
            if (sourceValue is null) return;

            TDestProperty transformedValue = castingFunction(sourceValue);
            destination.GetType().GetProperty(destinationProperty.Name)?.SetValue(destination, transformedValue);
        });

        return this;
    }

    public void Apply(TSource source, TDestination destination)
    {
        foreach (Action<TSource, TDestination> map in _mappings)
        {
            map(source, destination);
        }
    }
}