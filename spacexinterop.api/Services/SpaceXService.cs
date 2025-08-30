using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Response.External.Space_X;
using spacexinterop.api.Data.Request.External.Space_X;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Domain.Data.Errors;
using spacexinterop.api._Common.Utility.Extensions;
using spacexinterop.api.Repositories.Interfaces;
using spacexinterop.api.Services.Interfaces;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services;

public class SpaceXService(
    ISpaceXLaunchesRepository spaceXLaunchesRepository,
    ISpaceXClient client,
    IMapper mapper,
    ILogger<SpaceXService> logger,
    IResultFactory resultFactory
    ) : ISpaceXService
{
    public async Task<Result<PaginatedResponse<LaunchResponse>?>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            SpaceXQueryRequest queryRequest = new()
            {
                Query = new Dictionary<string, object> { [request.LaunchesRequestType.GetJsonPropertyName()] = true },
                Options = spaceXLaunchesRepository.CompleteLaunchesPaginated(request.PageSize, request.PageIndex)
            };

            SpaceXPaginatedResponse<Launch>? response = await client.GetQueryResponse<Launch>(queryRequest, cancellationToken);
            return resultFactory.Success(response?.ToPaginatedResponse<LaunchResponse>(mapper));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch upcoming launches from SpaceX API.");
            return resultFactory.Failure<PaginatedResponse<LaunchResponse>?>(CommonError.Unauthorized);
        }
    }
}