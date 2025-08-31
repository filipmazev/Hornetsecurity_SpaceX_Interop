using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Thrust
{
    [JsonPropertyName("kN")]
    public int Kn { get; set; }

    [JsonPropertyName("lbf")]
    public int Lbf { get; set; }
}