using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services.Interfaces;

public interface ISpaceXService
{
    Task<Result<PaginatedResponse<LaunchResponse>?>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default);
    Task<Result<LaunchResponse?>> GetLatestLaunch(CancellationToken cancellationToken = default);
}