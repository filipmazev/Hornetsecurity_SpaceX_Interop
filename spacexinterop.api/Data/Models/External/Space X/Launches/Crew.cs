using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class Crew : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "crew";
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CrewStatusEnum Status { get; set; }
    
    [JsonPropertyName("agency")]
    public string? Agency { get; set; }
    
    [JsonPropertyName("image")]
    public string? Image { get; set; }
    
    [JsonPropertyName("wikipedia")]
    public string? Wikipedia { get; set; }
    
    [JsonPropertyName("launches")]
    [JsonConverter(typeof(GuidOrObjectArrayConverter<Launch>))]
    public List<GuidOrObject<Launch>> Launches { get; set; } = [];
}