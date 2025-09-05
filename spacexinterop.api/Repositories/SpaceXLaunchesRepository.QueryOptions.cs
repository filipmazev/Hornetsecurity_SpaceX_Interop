using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Rockets;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Models.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Repositories;

public partial class SpaceXLaunchesRepository
{
    private QueryOptions SimpleLaunchesPaginatedQueryOptions(
        int pageSize, 
        int pageIndex, 
        SortDirectionEnum? sortDirection = SortDirectionEnum.Descending)
    {
        QueryOptions queryOptions = new()
        {
            Sort = sortDirection.HasValue 
                ? new SortOption().By<Launch>(launch => launch.DateUtc, sortDirection.Value) 
                : null,
            Populate =
            [
                PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
                    .Selecting<Rocket, string>(rocket => rocket.Name),
                
                PopulateOption.With<Launch, GuidOrObject<Launchpad>>(launch => launch.Launchpad!)
                    .Selecting<Launchpad, string?>(launchpad => launchpad.FullName)
            ],
            Offset = pageIndex * pageSize,
            Page = pageIndex,
            Limit = pageSize,
            Pagination = true
        };
        
        queryOptions.Populate.AddRange([
            _populateRocketOption,
            _populateLaunchpadOption
        ]);
        
        return queryOptions;
    }
    
    private QueryOptions CompleteLaunchQueryOptions()
    {
        QueryOptions queryOptions = new()
        {
            Sort = null,
            Populate =
            [
                PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
                    .Selecting<Rocket, string>(rocket => rocket.Name),
                
                PopulateOption.With<Launch, GuidOrObject<Launchpad>>(launch => launch.Launchpad!)
                    .Selecting<Launchpad, string?>(launchpad => launchpad.FullName)
            ],
            Offset = null,
            Page = null,
            Limit = 1,
            Pagination = false
        };
        
        queryOptions.Populate.AddRange([
            _populateRocketOption,
            _populateLaunchpadOption,
            _populatePayloadsOption,
            _populateLaunchCoresOption,
            _populateShipsOption,
            _populateCapsuleOption
        ]);
        
        return queryOptions;
    }
}