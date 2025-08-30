using spacexinterop.api.Data.External.Space_X.Launches;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services.Interfaces;

public interface ISpaceXService
{
    Task<Result<List<Launch>?>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default);
}