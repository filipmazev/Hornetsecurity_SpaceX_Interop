using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Utility.Clients.Interfaces;
using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Response.External.Space_X;
using spacexinterop.api.Data.Request.External.Space_X;
using spacexinterop.api._Common.Domain.Data.Errors;
using spacexinterop.api._Common.Domain.Data.Result;
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
    public async Task<Result<PaginatedResponse<LaunchRowResponse>?>> GetLaunchRows(SpaceXLaunchesRequest request)
    {
        try
        {
            bool hasTextSearch = !string.IsNullOrWhiteSpace(request.SearchText);

            List<string> keyParts =[nameof(Launch).ToLower() + "s", request.Upcoming.ToString(), request.PageIndex.ToString(), request.PageSize.ToString()];
            if (!hasTextSearch) keyParts.Add(request.SortDirection.ToString());
            keyParts.Add(request.SearchText ?? string.Empty);

            string cacheKey = string.Join("_", keyParts);
            
            if (memoryCache.TryGetValue(cacheKey, out PaginatedResponse<LaunchRowResponse>? cachedResult)) 
                return resultFactory.Success(cachedResult);

            SpaceXQueryRequest queryRequest = spaceXLaunchesRepository.SimpleLaunchesPaginated(
                searchText: request.SearchText,
                upcoming: request.Upcoming,
                pageSize: request.PageSize,
                pageIndex: request.PageIndex,
                sortDirection: request.SortDirection);

            SpaceXPaginatedResponse<Launch>? response = await client.GetQueryResponse<Launch>(queryRequest);
            PaginatedResponse<LaunchRowResponse>? result = response?.ToPaginatedResponse<LaunchRowResponse>(mapper);

            if(result is not null) memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(Constants.MemoryCacheTimeToLiveInMinutes));

            return resultFactory.Success(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch launches from SpaceX API.");
            return resultFactory.Failure<PaginatedResponse<LaunchRowResponse>?>(CommonError.SomethingWentWrong);
        }
    }
    
    public async Task<Result<CompleteLaunchResponse?>> GetCompleteLaunchById(string id)
    {
        try
        {
            string cacheKey = nameof(Launch).ToLower() + "s_" + id;
            
            if (memoryCache.TryGetValue(cacheKey, out CompleteLaunchResponse? cachedResult)) 
                return resultFactory.Success(cachedResult);

            SpaceXQueryRequest queryRequest = spaceXLaunchesRepository.CompleteLaunchById(id);

            SpaceXPaginatedResponse<Launch>? response = await client.GetQueryResponse<Launch>(queryRequest);
            PaginatedResponse<CompleteLaunchResponse>? paginatedResult = response?.ToPaginatedResponse<CompleteLaunchResponse>(mapper);
            CompleteLaunchResponse? result = paginatedResult?.Items.FirstOrDefault();
            
            if(result is not null) memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(Constants.MemoryCacheTimeToLiveInMinutes));
            else return resultFactory.Failure<CompleteLaunchResponse?>(CommonError.EntityNotFound(nameof(Launch)));
            
            return resultFactory.Success<CompleteLaunchResponse?>(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Failed to fetch complete launch with id: '{id}' from SpaceX API.");
            return resultFactory.Failure<CompleteLaunchResponse?>(CommonError.SomethingWentWrong);
        }
    }
}