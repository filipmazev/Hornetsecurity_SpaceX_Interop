using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum RequestTypeEnum
{
    [JsonPropertyName("query")]
    Query
}