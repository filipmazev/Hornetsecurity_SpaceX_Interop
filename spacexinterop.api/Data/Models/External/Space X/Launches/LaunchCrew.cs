using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api._Common.Utility.Converters;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Launches;

public class LaunchCrew : BaseJsonModel, IBaseJsonModel
{
    public string JsonPluralName => "crew";    
    
    [JsonPropertyName("crew")]
    [JsonConverter(typeof(GuidOrObjectConverter<Crew>))]
    public GuidOrObject<Crew>? Crew { get; set; }
    
    [JsonPropertyName("role")]
    public string? Role { get; set; }
}