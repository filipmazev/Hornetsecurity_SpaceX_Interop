using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Stage
{
    [JsonPropertyName("reusable")]
    public bool Reusable { get; set; }

    [JsonPropertyName("engines")]
    public int Engines { get; set; }

    [JsonPropertyName("fuel_amount_tons")]
    public double FuelAmountTons { get; set; }

    [JsonPropertyName("burn_time_sec")]
    public int? BurnTimeSec { get; set; }

    [JsonPropertyName("thrust_sea_level")]
    public Thrust ThrustSeaLevel { get; set; } = new();

    [JsonPropertyName("thrust_vacuum")]
    public Thrust ThrustVacuum { get; set; } = new();
}