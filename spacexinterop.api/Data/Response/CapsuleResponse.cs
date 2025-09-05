namespace spacexinterop.api.Data.Response;

public class CapsuleResponse
{
    public required string Serial { get; set; }
    public required string Status { get; set; }
    public required string Type { get; set; }
    public required int ReuseCount { get; set; }
    public required int WaterLandings { get; set; }
}