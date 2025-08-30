using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Rockets;

public class Mass
{
    [JsonPropertyName("kg")]
    public int Kg { get; set; }

    [JsonPropertyName("lb")]
    public int Lb { get; set; }
}