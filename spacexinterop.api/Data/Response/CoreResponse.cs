using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Data.Response;

public class CoreResponse
{
    public required string Serial { get; set; }
    public required int? Block { get; set; }
    public required CoreStatusEnum Status { get; set; }
    public required int ReuseCount { get; set; }
    public required string? LastUpdate { get; set; }
}