using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum CoreStatusEnum
{
    [JsonPropertyName("active")]
    Active,

    [JsonPropertyName("inactive")]
    Inactive,
    
    [JsonPropertyName("unknown")]
    Unknown,

    [JsonPropertyName("expended")]
    Expended,

    [JsonPropertyName("lost")]
    Lost,

    [JsonPropertyName("retired")]
    Retired
}