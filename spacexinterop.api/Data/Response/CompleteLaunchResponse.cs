using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Data.Response;

public class CompleteLaunchResponse
{
    public required string Id { get; set; }
    
    public required string Name { get; set; }
    public required int FlightNumber { get; set; }
    public required string RocketName { get; set; }
    public required string LaunchpadName { get; set; }

    public required DateTime StaticFireDateUtc { get; set; }
    public required DateTime LaunchDateUtc { get; set; }
    public required DatePrecisionEnum DatePrecision { get; set; }  

    public required string? Details { get; set; }
    public required bool Upcoming { get; set; }
    public required bool? Success { get; set; }
    public required List<string> FailureReasons { get; set; } = [];

    public required List<LaunchCoreResponse> Cores { get; set; } = [];
    public required List<PayloadResponse> Payloads { get; set; } = [];
    public required List<ShipResponse> Ships { get; set; } = [];
    public required List<CapsuleResponse> Capsules { get; set; } = [];

    public required LaunchLinksResponse? Links { get; set;  }
}