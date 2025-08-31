using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X;

public class Ship : BaseJsonModel
{
    public override string JsonPluralName => "ships";

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("legacy_id")]
    public string? LegacyId { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = [];

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("imo")]
    public long? Imo { get; set; }

    [JsonPropertyName("mmsi")]
    public long? Mmsi { get; set; }

    [JsonPropertyName("abs")]
    public int? Abs { get; set; }

    [JsonPropertyName("class")]
    public int? Class { get; set; }

    [JsonPropertyName("mass_kg")]
    public double? MassKg { get; set; }

    [JsonPropertyName("mass_lbs")]
    public double? MassLbs { get; set; }

    [JsonPropertyName("year_built")]
    public int? YearBuilt { get; set; }

    [JsonPropertyName("home_port")]
    public string? HomePort { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("speed_kn")]
    public double? SpeedKn { get; set; }

    [JsonPropertyName("course_deg")]
    public double? CourseDeg { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("last_ais_update")]
    public string? LastAisUpdate { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("launches")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Launch>))]
    public List<GuidOrObject<Launch>> Launches { get; set; } = [];
}