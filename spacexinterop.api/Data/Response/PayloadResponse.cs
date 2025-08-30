namespace spacexinterop.api.Data.Response;

public class PayloadResponse
{
    public required string? Name { get; set; }
    public required string? Type { get; set; }
    public required bool Reused { get; set; }
    public required List<string> Customers { get; set; } = [];
    public required List<string> Manufacturers { get; set; } = [];
}