using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Rockets;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Launch : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "launches";

    [JsonPropertyName("flight_number")]
    public int FlightNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("date_utc")]
    public DateTime DateUtc { get; set; }

    [JsonPropertyName("date_unix")]
    public long DateUnix { get; set; }

    [JsonPropertyName("date_local")]
    public DateTime DateLocal { get; set; }

    [JsonPropertyName("date_precision")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DatePrecisionEnum DatePrecision { get; set; }

    [JsonPropertyName("static_fire_date_utc")]
    public DateTime? StaticFireDateUtc { get; set; }

    [JsonPropertyName("static_fire_date_unix")]
    public long? StaticFireDateUnix { get; set; }

    [JsonPropertyName("tbd")]
    public bool Tbd { get; set; }

    [JsonPropertyName("net")]
    public bool Net { get; set; }

    [JsonPropertyName("window")]
    public int? Window { get; set; }

    [JsonPropertyName("rocket")]
    [JsonConverter(typeof(GuidOrObjectConverter<Rocket>))]
    public GuidOrObject<Rocket>? Rocket { get; set; }

    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    [JsonPropertyName("failures")]
    public List<Failure> Failures { get; set; } = [];

    [Required]
    [JsonPropertyName("upcoming")]
    public bool Upcoming { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("fairings")]
    public Fairings? Fairings { get; set; }

    [JsonPropertyName("crew")]
    public List<LaunchCrew> Crew { get; set; } = [];

    [JsonPropertyName("ships")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Ship>))]
    public List<GuidOrObject<Ship>> Ships { get; set; } = [];

    [JsonPropertyName("capsules")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Capsule>))]
    public List<GuidOrObject<Capsule>> Capsules { get; set; } = [];

    [JsonPropertyName("payloads")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Payload>))]
    public List<GuidOrObject<Payload>> Payloads { get; set; } = [];

    [JsonPropertyName("launchpad")]
    [JsonConverter(typeof(GuidOrObjectConverter<Launchpad>))]
    public GuidOrObject<Launchpad>? Launchpad { get; set; }

    [JsonPropertyName("cores")]
    public List<LaunchCore> Cores { get; set; } = [];

    [JsonPropertyName("links")]
    public Links? Links { get; set; }

    [JsonPropertyName("auto_update")]
    public bool AutoUpdate { get; set; } = true;
}