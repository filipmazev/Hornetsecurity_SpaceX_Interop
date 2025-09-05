using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace spacexinterop.api._Common.Utility.Helpers;

public static class JsonDictionaryHelper
{
    public static Dictionary<string, object?> ToJsonDictionary<TModel>(TModel model)
        where TModel : IBaseJsonQueryModel
    {
        string json = JsonSerializer.Serialize(
            model,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

        using JsonDocument doc = JsonDocument.Parse(json);
        return ConvertElement(doc.RootElement);
    }

    private static Dictionary<string, object?> ConvertElement(JsonElement element)
    {
        Dictionary<string, object?> dict = new();

        foreach (JsonProperty prop in element.EnumerateObject())
        {
            dict[prop.Name] = ConvertJsonValue(prop.Value);
        }

        return dict;
    }

    private static object? ConvertJsonValue(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.GetDouble(), 
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Object => ConvertElement(element),
            JsonValueKind.Array => element.EnumerateArray().Select(ConvertJsonValue).ToList(),
            JsonValueKind.Null => null,
            _ => element.GetRawText()
        };
    }
}