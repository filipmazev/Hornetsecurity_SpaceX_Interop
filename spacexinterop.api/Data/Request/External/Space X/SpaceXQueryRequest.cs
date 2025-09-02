using spacexinterop.api.Data.Models.External.Space_X.Core;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Request.External.Space_X;

public class SpaceXQueryRequest
{
    [JsonPropertyName("query")]
    public Dictionary<string, object> Query { get; set; } = new();

    [JsonPropertyName("options")]
    public QueryOptions Options { get; set; } = new();
}