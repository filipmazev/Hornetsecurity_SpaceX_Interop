using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Request.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api._Common.Utility.Extensions;
using spacexinterop.api._Common.Utility.Helpers;
using spacexinterop.api.Repositories.Interfaces;
using spacexinterop.api._Common;

namespace spacexinterop.api.Repositories;

public partial class SpaceXLaunchesRepository : ISpaceXLaunchesRepository
{
    public SpaceXQueryRequest SimpleLaunchesPaginated(
        string? searchText,
        bool upcoming,
        int pageSize, 
        int pageIndex,
        SortDirectionEnum? sortDirection = SortDirectionEnum.Descending)
    {
        bool hasSearchText = !string.IsNullOrWhiteSpace(searchText);
        
        SpaceXQueryRequest queryRequest = new()
        {
            Query = new Dictionary<string, object>
            {
                [LaunchesRequestTypeEnum.Upcoming.GetJsonPropertyName()] = upcoming
            },
            Options = SimpleLaunchesPaginatedQueryOptions(
                pageSize: pageSize, 
                pageIndex: pageIndex, 
                sortDirection: hasSearchText ? null : sortDirection)
        };

        if (hasSearchText)
        {
            TextSearchQuery textSearchQuery = new(searchText);
            queryRequest.Query.TryAdd(textSearchQuery.JsonQueryFieldName, JsonDictionaryHelper.ToJsonDictionary(textSearchQuery));
        }

        return queryRequest;
    }
    
    public SpaceXQueryRequest CompleteLaunchById(string id)
    {
        SpaceXQueryRequest queryRequest = new()
        {
            Query = new Dictionary<string, object>
            {
                [Constants.MongoDbIdPropertyName] = id
            },
            Options = CompleteLaunchQueryOptions()
        };

        return queryRequest;
    }
}