using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Data.Response;

public class LatestLaunchResponse
{
    public required string Name { get; set; }
    public required int FlightNumber { get; set; }
    
    public required DateTime StaticFireDateUtc { get; set; }
    public required DateTime LaunchDateUtc { get; set; }
    public required DatePrecisionEnum DatePrecision { get; set; }
    
    public required string? Details { get; set; }
    public required bool Upcoming { get; set; }
    public required bool? Success { get; set; }
    
    public required List<string> FailureReasons { get; set; } = [];
    public required List<CoreResponse> Cores { get; set; } = [];
    
    public required RedditResponse? Reddit { get; set; }
    
    public required List<string> FlickerImagesOriginal { get; set; } = [];
    
    public required string? MissionPatchImageSmall { get; set; }
    public required string? MissionPatchImageOriginal { get; set; }
    
    public required string? PressKitUrl { get; set; }
    public required string? WebcastUrl { get; set; }
    public required string? ArticleUrl { get; set; }
    public required string? WikipediaUrl { get; set; }
}