namespace spacexinterop.api.Data.Response;

public class LaunchLinksResponse
{
    public required RedditResponse? Reddit { get; set; }
    public required List<string> FlickerImagesSmall { get; set; } = [];
    public required List<string> FlickerImagesOriginal { get; set; } = [];
    public required string? MissionPatchImageSmall { get; set; }
    public required string? MissionPatchImageOriginal { get; set; }
    public required string? PressKitUrl { get; set; }
    public required string? WebcastUrl { get; set; }
    public required string? ArticleUrl { get; set; }
    public required string? WikipediaUrl { get; set; }
}