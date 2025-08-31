using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class CompositeFairing
{
    [JsonPropertyName("height")]
    public Dimension Height { get; set; } = new();

    [JsonPropertyName("diameter")]
    public Dimension Diameter { get; set; } = new();
}