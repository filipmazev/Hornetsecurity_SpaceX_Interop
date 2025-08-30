using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Links
{
    [JsonPropertyName("patch")]
    public Patch? Patch { get; set; }

    [JsonPropertyName("reddit")]
    public Reddit? Reddit { get; set; }

    [JsonPropertyName("flickr")]
    public Flickr? Flickr { get; set; }

    [JsonPropertyName("presskit")]
    public string? Presskit { get; set; }

    [JsonPropertyName("webcast")]
    public string? Webcast { get; set; }

    [JsonPropertyName("youtube_id")]
    public string? YoutubeId { get; set; }

    [JsonPropertyName("article")]
    public string? Article { get; set; }

    [JsonPropertyName("wikipedia")]
    public string? Wikipedia { get; set; }
}