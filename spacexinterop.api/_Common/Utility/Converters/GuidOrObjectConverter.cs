using spacexinterop.api.Data.Models.External.Space_X.Core;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace spacexinterop.api._Common.Utility.Converters;

public class GuidOrObjectConverter<TModel> : JsonConverter<GuidOrObject<TModel>>
    where TModel : BaseJsonModel
{
    public override GuidOrObject<TModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                string? guid = reader.GetString();
                return guid is not null
                    ? new GuidOrObject<TModel> { Guid = guid }
                    : null;
            }
            case JsonTokenType.StartObject:
            {
                TModel? obj = JsonSerializer.Deserialize<TModel>(ref reader, options);
                return obj is not null
                    ? new GuidOrObject<TModel> { Object = obj }
                    : null;
            }
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, GuidOrObject<TModel> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        if (value.Guid is not null)
            writer.WriteStringValue(value.Guid);
        else if (value.Object is not null)
            JsonSerializer.Serialize(writer, value.Object, options);
    }
}