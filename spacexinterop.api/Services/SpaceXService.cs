using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api.Data.External.Space_X.Launches;
using spacexinterop.api._Common.Domain.Data.Errors;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Services.Interfaces;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services;

public class SpaceXService(
    ISpaceXClient client,
    ILogger<SpaceXService> logger,
    IResultFactory resultFactory
    ) : ISpaceXService
{
    public async Task<Result<List<Launch>?>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            List<Launch> launches = await client.GetLaunches(request, cancellationToken);
            return resultFactory.Success<List<Launch>?>(launches);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch upcoming launches from SpaceX API.");
            return resultFactory.Failure<List<Launch>?>(CommonError.Unauthorized);
        }
    }
}