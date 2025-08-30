using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Payloads
{
    [JsonPropertyName("option_1")]
    public string Option1 { get; set; } = string.Empty;

    [JsonPropertyName("composite_fairing")]
    public CompositeFairing CompositeFairing { get; set; } = new();
}