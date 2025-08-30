using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class LandingLegs
{
    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("material")]
    public string? Material { get; set; }
}