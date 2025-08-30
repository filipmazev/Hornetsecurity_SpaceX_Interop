using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X;

public class Dragon : BaseJsonModel
{
    public override string JsonPluralName => "dragons";

    [JsonPropertyName("capsule")]
    [JsonConverter(typeof(GuidOrObjectConverter<Capsule>))]
    public GuidOrObject<Capsule>? Capsule { get; set; }

    [JsonPropertyName("mass_returned_kg")]
    public double? MassReturnedKg { get; set; }

    [JsonPropertyName("mass_returned_lbs")]
    public double? MassReturnedLbs { get; set; }

    [JsonPropertyName("flight_time_sec")]
    public double? FlightTimeSec { get; set; }

    [JsonPropertyName("manifest")]
    public string? Manifest { get; set; }

    [JsonPropertyName("water_landing")]
    public bool? WaterLanding { get; set; }

    [JsonPropertyName("land_landing")]
    public bool? LandLanding { get; set; }
}