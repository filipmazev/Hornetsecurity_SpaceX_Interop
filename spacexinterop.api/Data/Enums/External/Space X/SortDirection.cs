using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum SortDirection
{
    [JsonPropertyName("asc")]
    Ascending,

    [JsonPropertyName("desc")]
    Descending
}