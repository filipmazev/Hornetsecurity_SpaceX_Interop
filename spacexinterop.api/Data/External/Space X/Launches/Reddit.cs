using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.External.Space_X.Launches;

public class Reddit
{
    [JsonPropertyName("campaign")]
    public string? Campaign { get; set; }

    [JsonPropertyName("launch")]
    public string? Launch { get; set; }

    [JsonPropertyName("media")]
    public string? Media { get; set; }

    [JsonPropertyName("recovery")]
    public string? Recovery { get; set; }
}