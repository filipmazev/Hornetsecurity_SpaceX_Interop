using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X;

public class Capsule : BaseJsonModel
{
    public override string JsonPluralName => "capsules";

    [JsonPropertyName("serial")]
    public string Serial { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("dragon")]
    [JsonConverter(typeof(GuidOrObjectConverter<Dragon>))]
    public GuidOrObject<Dragon>? Dragon { get; set; }

    [JsonPropertyName("reuse_count")]
    public int ReuseCount { get; set; } = 0;

    [JsonPropertyName("water_landings")]
    public int WaterLandings { get; set; } = 0;

    [JsonPropertyName("land_landings")]
    public int LandLandings { get; set; } = 0;

    [JsonPropertyName("last_update")]
    public string? LastUpdate { get; set; }

    [JsonPropertyName("launches")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Launch>))]
    public List<GuidOrObject<Launch>>? Launches { get; set; }
}