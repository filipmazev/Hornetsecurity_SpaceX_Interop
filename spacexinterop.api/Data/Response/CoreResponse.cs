namespace spacexinterop.api.Data.Response;

public class CoreResponse
{
    public required int? Flight { get; set; }
    public required bool? GridFins { get; set; }
    public required bool? Legs { get; set; }
    public required bool? Reused { get; set; }
    public required bool? LandingAttempt { get; set; }
    public required bool? LandingSuccess { get; set; }
    public required string? LandingType { get; set; }
}