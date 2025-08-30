using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Enums.External.Space_X;

public enum LaunchesRequestTypeEnum
{
    [JsonPropertyName("latest")]
    Latest,

    [JsonPropertyName("upcoming")]
    Upcoming,

    [JsonPropertyName("past")]
    Past
}