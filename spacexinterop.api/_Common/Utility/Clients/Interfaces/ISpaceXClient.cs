using spacexinterop.api.Data.External.Space_X.Launches;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api._Common.Utility.Clients.Interfaces;

public interface ISpaceXClient
{
    Task<List<Launch>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default);
}