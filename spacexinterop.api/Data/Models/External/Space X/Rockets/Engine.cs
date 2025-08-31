using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Engine
{
    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("layout")]
    public string? Layout { get; set; }

    [JsonPropertyName("isp")]
    public Isp Isp { get; set; } = new();

    [JsonPropertyName("engine_loss_max")]
    public int? EngineLossMax { get; set; }

    [JsonPropertyName("propellant_1")]
    public string Propellant1 { get; set; } = string.Empty;

    [JsonPropertyName("propellant_2")]
    public string Propellant2 { get; set; } = string.Empty;

    [JsonPropertyName("thrust_sea_level")]
    public Thrust ThrustSeaLevel { get; set; } = new();

    [JsonPropertyName("thrust_vacuum")]
    public Thrust ThrustVacuum { get; set; } = new();

    [JsonPropertyName("thrust_to_weight")]
    public double ThrustToWeight { get; set; }
}