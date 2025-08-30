using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using spacexinterop.api.Data.Enums;

namespace spacexinterop.api.Data.External.Space_X.Launches;

public class Launch
{
    [Required]
    [JsonPropertyName("flight_number")]
    public int FlightNumber { get; set; }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("date_utc")]
    public DateTime DateUtc { get; set; }

    [Required]
    [JsonPropertyName("date_unix")]
    public long DateUnix { get; set; }

    [Required]
    [JsonPropertyName("date_local")]
    public DateTime DateLocal { get; set; }

    [Required]
    [JsonPropertyName("date_precision")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DatePrecisionEnum DatePrecision { get; set; }

    [JsonPropertyName("static_fire_date_utc")]
    public DateTime? StaticFireDateUtc { get; set; }

    [JsonPropertyName("static_fire_date_unix")]
    public long? StaticFireDateUnix { get; set; }

    [JsonPropertyName("tbd")]
    public bool Tbd { get; set; } = false;

    [JsonPropertyName("net")]
    public bool Net { get; set; } = false;

    [JsonPropertyName("window")]
    public int? Window { get; set; }

    [JsonPropertyName("rocket")]
    public string? Rocket { get; set; }

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
    public List<CrewMember> Crew { get; set; } = [];

    [JsonPropertyName("ships")]
    public List<string> Ships { get; set; } = [];

    [JsonPropertyName("capsules")]
    public List<string> Capsules { get; set; } = [];

    [JsonPropertyName("payloads")]
    public List<string> Payloads { get; set; } = [];

    [JsonPropertyName("launchpad")]
    public string? Launchpad { get; set; }

    [JsonPropertyName("cores")]
    public List<Core> Cores { get; set; } = [];

    [JsonPropertyName("links")]
    public Links? Links { get; set; }

    [JsonPropertyName("auto_update")]
    public bool AutoUpdate { get; set; } = true;
}