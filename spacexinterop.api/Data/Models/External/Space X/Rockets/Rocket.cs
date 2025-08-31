using spacexinterop.api.Data.Models.External.Space_X.Core;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Rocket : BaseJsonModel
{
    public override string JsonPluralName => "rockets";

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("stages")]
    public int Stages { get; set; }

    [JsonPropertyName("boosters")]
    public int Boosters { get; set; }

    [JsonPropertyName("cost_per_launch")]
    public int CostPerLaunch { get; set; }

    [JsonPropertyName("success_rate_pct")]
    public int SuccessRatePct { get; set; }

    [JsonPropertyName("first_flight")]
    public string FirstFlight { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("company")]
    public string Company { get; set; } = string.Empty;

    [JsonPropertyName("height")]
    public Dimension Height { get; set; } = new();

    [JsonPropertyName("diameter")]
    public Dimension Diameter { get; set; } = new();

    [JsonPropertyName("mass")]
    public Mass Mass { get; set; } = new();

    [JsonPropertyName("payload_weights")]
    public List<PayloadWeight>? PayloadWeights { get; set; }

    [JsonPropertyName("first_stage")]
    public Stage FirstStage { get; set; } = new();

    [JsonPropertyName("second_stage")]
    public SecondStage SecondStage { get; set; } = new();

    [JsonPropertyName("engines")]
    public Engine Engines { get; set; } = new();

    [JsonPropertyName("landing_legs")]
    public LandingLegs LandingLegs { get; set; } = new();

    [JsonPropertyName("flickr_images")]
    public List<string> FlickrImages { get; set; } = [];

    [JsonPropertyName("wikipedia")]
    public string Wikipedia { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}