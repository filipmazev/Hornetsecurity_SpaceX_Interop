using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Extensions;
using System.Text.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public class SortOption
{
    [JsonExtensionData]
    public Dictionary<string, object> Fields { get; } = new();

    public SortOption By<TModel>(Expression<Func<TModel, object>> property, SortDirectionEnum direction)
        where TModel : BaseJsonModel, IBaseJsonModel
    {
        string name = GetJsonPropertyName(property);
        Fields[name] = direction.GetJsonPropertyName();
        return this;
    }

    private static string GetJsonPropertyName<TModel>(Expression<Func<TModel, object>> property)
        where TModel : BaseJsonModel, IBaseJsonModel
    {
        MemberExpression? member = property.Body as MemberExpression
                                   ?? ((UnaryExpression)property.Body).Operand as MemberExpression;

        JsonPropertyNameAttribute? attr = member?.Member.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attr?.Name ?? member?.Member.Name ?? throw new InvalidOperationException();
    }
}