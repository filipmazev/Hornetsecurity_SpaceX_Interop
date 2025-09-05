using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Core : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "cores";

    [Required]
    [JsonPropertyName("serial")]
    public string Serial { get; set; } = string.Empty;

    [JsonPropertyName("block")]
    public int? Block { get; set; }

    [Required]
    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CoreStatusEnum Status { get; set; }

    [JsonPropertyName("reuse_count")]
    public int ReuseCount { get; set; } = 0;

    [JsonPropertyName("rtls_attempts")]
    public int RtlsAttempts { get; set; } = 0;

    [JsonPropertyName("rtls_landings")]
    public int RtlsLandings { get; set; } = 0;

    [JsonPropertyName("asds_attempts")]
    public int AsdsAttempts { get; set; } = 0;

    [JsonPropertyName("asds_landings")]
    public int AsdsLandings { get; set; } = 0;

    [JsonPropertyName("last_update")]
    public string? LastUpdate { get; set; }

    [JsonPropertyName("launches")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Launch>))]
    public List<GuidOrObject<Launch>> Launches { get; set; } = [];
}