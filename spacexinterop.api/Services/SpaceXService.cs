using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Response.External.Space_X;
using spacexinterop.api.Data.Request.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Domain.Data.Errors;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Utility.Extensions;
using spacexinterop.api.Repositories.Interfaces;
using spacexinterop.api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;
using spacexinterop.api._Common;

namespace spacexinterop.api.Services;

public class SpaceXService(
    ISpaceXLaunchesRepository spaceXLaunchesRepository,
    ISpaceXClient client,
    IMapper mapper,
    ILogger<SpaceXService> logger,
    IResultFactory resultFactory,
    IMemoryCache memoryCache
    ) : ISpaceXService
{
    public async Task<Result<PaginatedResponse<LaunchResponse>?>> GetLaunches(SpaceXLaunchesRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            string cacheKey = $"{nameof(Launch).ToLower()}s_{request.PageIndex}_{request.PageSize}_{request.SortDirection}_{request.IncludePayloads}";

            if (memoryCache.TryGetValue(cacheKey, out PaginatedResponse<LaunchResponse>? cachedResult))
            {
                return resultFactory.Success(cachedResult);
            }

            SpaceXQueryRequest queryRequest = new()
            {
                Query = new Dictionary<string, object> { [LaunchesRequestTypeEnum.Upcoming.GetJsonPropertyName()] = request.Upcoming },
                Options = spaceXLaunchesRepository.CompleteLaunchesPaginated(request.PageSize, request.PageIndex, request.SortDirection, request.IncludePayloads)
            };

            SpaceXPaginatedResponse<Launch>? response = await client.GetQueryResponse<Launch>(queryRequest, cancellationToken);
            PaginatedResponse<LaunchResponse>? result = response?.ToPaginatedResponse<LaunchResponse>(mapper);

            if(result is not null) memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(Constants.MemoryCacheTimeToLiveInMinutes));

            return resultFactory.Success(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch upcoming launches from SpaceX API.");
            return resultFactory.Failure<PaginatedResponse<LaunchResponse>?>(CommonError.Unauthorized);
        }
    }
}