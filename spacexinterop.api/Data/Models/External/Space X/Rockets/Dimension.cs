using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Dimension
{
    [JsonPropertyName("meters")]
    public double? Meters { get; set; }

    [JsonPropertyName("feet")]
    public double? Feet { get; set; }
}