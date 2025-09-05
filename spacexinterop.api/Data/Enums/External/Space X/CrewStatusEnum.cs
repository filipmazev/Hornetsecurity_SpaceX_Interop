using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum CrewStatusEnum
{
    [JsonPropertyName("active")]
    Active,

    [JsonPropertyName("inactive")]
    Inactive,
    
    [JsonPropertyName("retired")]
    Retired,

    [JsonPropertyName("unkown")]
    Unknown
}