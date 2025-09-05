using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace spacexinterop.api._Common.Utility.Converters;

public class GuidOrObjectArrayConverter<TModel> : JsonConverter<List<GuidOrObject<TModel>>>
    where TModel : BaseJsonModel, IBaseJsonModel
{
    public override List<GuidOrObject<TModel>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                string? guid = reader.GetString();
                return guid is not null
                    ? [new GuidOrObject<TModel> { Guid = guid }]
                    : null;
            }
            case JsonTokenType.StartArray:
            {
                using JsonDocument doc = JsonDocument.ParseValue(ref reader);

                if (doc.RootElement.ValueKind != JsonValueKind.Array) return null;

                if (doc.RootElement.GetArrayLength() > 0 && doc.RootElement[0].ValueKind == JsonValueKind.String)
                {
                    return doc.RootElement.EnumerateArray()
                        .Select(item => new GuidOrObject<TModel> { Guid = item.GetString() })
                        .ToList();
                }

                return doc.RootElement.EnumerateArray()
                    .Select(item => new GuidOrObject<TModel>
                    {
                        Object = JsonSerializer.Deserialize<TModel>(item.GetRawText(), options)
                    })
                    .ToList();
            }
            case JsonTokenType.StartObject:
            {
                TModel? obj = JsonSerializer.Deserialize<TModel>(ref reader, options);
                return obj is not null
                    ? [new GuidOrObject<TModel> { Object = obj }]
                    : null;
            }
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, List<GuidOrObject<TModel>> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (GuidOrObject<TModel> item in value)
        {
            if (item.Guid is not null) writer.WriteStringValue(item.Guid);
            else if (item.Object is not null) JsonSerializer.Serialize(writer, item.Object, options);
        }

        writer.WriteEndArray();
    }
}