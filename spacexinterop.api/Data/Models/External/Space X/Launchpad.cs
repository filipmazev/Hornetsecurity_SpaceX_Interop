using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Rockets;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X;

public class Launchpad : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "launchpads";

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LaunchpadStatusEnum Status { get; set; }

    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("launch_attempts")]
    public int LaunchAttempts { get; set; } = 0;

    [JsonPropertyName("launch_successes")]
    public int LaunchSuccesses { get; set; } = 0;

    [JsonPropertyName("rockets")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Rocket>))]
    public List<GuidOrObject<Rocket>> Rockets { get; set; } = [];

    [JsonPropertyName("launches")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Launch>))]
    public List<GuidOrObject<Launch>> Launches { get; set; } = [];
}
