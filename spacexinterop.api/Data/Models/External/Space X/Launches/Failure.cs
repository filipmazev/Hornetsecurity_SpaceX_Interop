using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Failure
{
    [JsonPropertyName("time")]
    public int? Time { get; set; }

    [JsonPropertyName("altitude")]
    public int? Altitude { get; set; }
    
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}