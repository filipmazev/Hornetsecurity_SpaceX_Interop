using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum LandpadStatusEnum
{
    [JsonPropertyName("active")]
    Active,

    [JsonPropertyName("inactive")]
    Inactive,

    [JsonPropertyName("unknown")]
    Unknown,

    [JsonPropertyName("retired")]
    Retired,

    [JsonPropertyName("lost")]
    Lost,

    [JsonPropertyName("under construction")]
    UnderConstruction
}