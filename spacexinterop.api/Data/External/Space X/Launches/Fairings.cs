using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.External.Space_X.Launches;

public class Fairings
{
    [JsonPropertyName("reused")]
    public bool? Reused { get; set; }

    [JsonPropertyName("recovery_attempt")]
    public bool? RecoveryAttempt { get; set; }

    [JsonPropertyName("recovered")]
    public bool? Recovered { get; set; }

    [JsonPropertyName("ships")]
    public List<string> Ships { get; set; } = [];
}