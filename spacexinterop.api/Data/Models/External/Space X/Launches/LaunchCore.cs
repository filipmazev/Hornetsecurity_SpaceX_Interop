using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class LaunchCore : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "cores";    
    
    [JsonPropertyName("core")]
    [JsonConverter(typeof(GuidOrObjectConverter<Core>))]
    public GuidOrObject<Core>? Core { get; set; }

    [JsonPropertyName("flight")]
    public int? FlightNumber { get; set; }

    [JsonPropertyName("gridfins")]
    public bool? Gridfins { get; set; }

    [JsonPropertyName("legs")]
    public bool? Legs { get; set; }

    [JsonPropertyName("reused")]
    public bool? Reused { get; set; }

    [JsonPropertyName("landing_attempt")]
    public bool? LandingAttempt { get; set; }

    [JsonPropertyName("landing_success")]
    public bool? LandingSuccess { get; set; }

    [JsonPropertyName("landing_type")]
    public string? LandingType { get; set; }

    [JsonPropertyName("landpad")]
    [JsonConverter(typeof(GuidOrObjectConverter<Landpad>))]
    public GuidOrObject<Landpad>? Landpad { get; set; }
}