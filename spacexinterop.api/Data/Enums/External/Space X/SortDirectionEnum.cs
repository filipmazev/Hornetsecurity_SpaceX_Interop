using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum SortDirectionEnum
{
    [JsonPropertyName("asc")]
    Ascending,

    [JsonPropertyName("desc")]
    Descending
}