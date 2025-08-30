using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class CrewMember
{
    [JsonPropertyName("crew")]
    public string? Crew { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }
}