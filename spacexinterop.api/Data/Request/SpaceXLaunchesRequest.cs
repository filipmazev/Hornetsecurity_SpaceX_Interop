using System.ComponentModel.DataAnnotations;
using spacexinterop.api.Data.Enums;

namespace spacexinterop.api.Data.Request;

public class SpaceXLaunchesRequest
{
    [Required]
    public required SpaceXLaunchesRequestTypeEnum LaunchesRequestType { get; set; }
}