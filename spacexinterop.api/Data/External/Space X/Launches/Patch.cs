using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.External.Space_X.Launches;

public class Patch
{
    [JsonPropertyName("small")]
    public string? Small { get; set; }

    [JsonPropertyName("large")]
    public string? Large { get; set; }
}