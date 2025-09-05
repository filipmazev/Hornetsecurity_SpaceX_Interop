using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public class BaseJsonModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}