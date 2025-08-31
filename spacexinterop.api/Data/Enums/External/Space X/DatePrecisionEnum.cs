using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum DatePrecisionEnum
{
    [JsonPropertyName("half")]
    Half,

    [JsonPropertyName("quarter")]
    Quarter,

    [JsonPropertyName("year")]
    Year,

    [JsonPropertyName("month")]
    Month,

    [JsonPropertyName("day")]
    Day,

    [JsonPropertyName("hour")]
    Hour
}