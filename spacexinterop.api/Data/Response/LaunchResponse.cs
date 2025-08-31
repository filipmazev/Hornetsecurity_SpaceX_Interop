using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Data.Response;

public class LaunchResponse
{
    public required string Name { get; set; }
    public required string RocketName { get; set; }
    public required string LaunchpadName { get; set; }

    public required DateTime LaunchDateUtc { get; set; }
    public required DatePrecisionEnum DatePrecision { get; set; }  

    public required string? Details { get; set; }
    public required bool Upcoming { get; set; }
    public required bool? Success { get; set; }
    public required List<string> FailureReasons { get; set; } = [];

    public required string? MissionPatchImage { get; set; }
    public required string? WebcastUrl { get; set; }
    public required string? WikipediaUrl { get; set; }
    public required string? ArticleUrl { get; set; }

    public required List<PayloadResponse> Payloads { get; set; } = [];
}