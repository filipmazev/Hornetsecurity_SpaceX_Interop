using System.Text.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;
using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public class PopulateOption
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("select")]
    public Dictionary<string, object>? Select { get; set; }

    [JsonPropertyName("populate")] 
    public List<PopulateOption> Nested { get; set; } = [];

    public static PopulateOption With<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        where TModel : BaseJsonModel, IBaseJsonModel
    {
        return new PopulateOption
        {
            Path = GetJsonPropertyName(property)
        };
    }

    public PopulateOption Selecting<TModel, TProperty>(Expression<Func<TModel, TProperty>> property, bool include = true)
        where TModel : BaseJsonModel, IBaseJsonModel
    {
        Select ??= new Dictionary<string, object>();
        Select[GetJsonPropertyName(property)] = include ? 1 : 0;
        return this;
    }

    public PopulateOption PopulateNested<TModel, TNested>(Expression<Func<TModel, TNested>> property, Action<PopulateOption> configure)
        where TModel : BaseJsonModel, IBaseJsonModel
    {
        PopulateOption nested = new PopulateOption
        {
            Path = GetJsonPropertyName(property)
        };
        
        configure(nested);
        Nested.Add(nested);
        return this;
    }

    private static string GetJsonPropertyName(Expression expression)
    {
        if (expression is not LambdaExpression lambda)
            throw new InvalidOperationException("Expression is not a lambda");
        
        MemberExpression? member = lambda.Body as MemberExpression
                                   ?? ((UnaryExpression)lambda.Body).Operand as MemberExpression;

        JsonPropertyNameAttribute? attr = member?.Member.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attr?.Name ?? member?.Member.Name ?? throw new InvalidOperationException("Could not resolve property name");
    }
}