using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public class QueryOptions
{
    [JsonPropertyName("sort")]
    public SortOption? Sort { get; set; }

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("pagination")]
    public bool? Pagination { get; set; }

    [JsonPropertyName("populate")]
    public List<PopulateOption>? Populate { get; set; }
}