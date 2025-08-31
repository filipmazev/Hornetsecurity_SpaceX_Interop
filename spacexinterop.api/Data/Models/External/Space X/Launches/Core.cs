using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Core
{
    [JsonPropertyName("core")]
    public string? CoreId { get; set; }

    [JsonPropertyName("flight")]
    public int? FlightNumber { get; set; }

    [JsonPropertyName("gridfins")]
    public bool? Gridfins { get; set; }

    [JsonPropertyName("legs")]
    public bool? Legs { get; set; }

    [JsonPropertyName("reused")]
    public bool? Reused { get; set; }

    [JsonPropertyName("landing_attempt")]
    public bool? LandingAttempt { get; set; }

    [JsonPropertyName("landing_success")]
    public bool? LandingSuccess { get; set; }

    [JsonPropertyName("landing_type")]
    public string? LandingType { get; set; }

    [JsonPropertyName("landpad")]
    public string? Landpad { get; set; }
}