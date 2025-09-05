using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External;

public class Landpad : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "landpads";

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LandpadStatusEnum Status { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("landing_attempts")]
    public int LandingAttempts { get; set; }

    [JsonPropertyName("landing_successes")]
    public int LandingSuccesses { get; set; }

    [JsonPropertyName("wikipedia")]
    public string? Wikipedia { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("launches")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Launch>))]
    public List<GuidOrObject<Launch>> Launches { get; set; } = [];
}