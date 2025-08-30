using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class SecondStage : Stage
{
    [JsonPropertyName("thrust")]
    public Thrust Thrust { get; set; } = new();

    [JsonPropertyName("payloads")]
    public Payloads Payloads { get; set; } = new();
}