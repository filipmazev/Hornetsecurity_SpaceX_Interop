using spacexinterop.api.Data.Enums.External.Space_X;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Request;

public class SpaceXLaunchesRequest
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required LaunchesRequestTypeEnum LaunchesRequestType { get; set; }

    [Required]
    public required int PageIndex { get; set; }

    [Required]
    public required int PageSize { get; set; }
}