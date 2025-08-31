using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Flickr
{
    [JsonPropertyName("small")]
    public List<string> Small { get; set; } = [];

    [JsonPropertyName("original")]
    public List<string> Original { get; set; } = [];
}