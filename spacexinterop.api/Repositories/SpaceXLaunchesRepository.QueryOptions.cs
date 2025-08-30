using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Rockets;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Models.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Repositories;

public partial class SpaceXLaunchesRepository
{
    public QueryOptions CompleteLaunchesPaginated(
        int pageSize, 
        int pageIndex, 
        SortDirectionEnum sortDirection = SortDirectionEnum.Descending,
        bool includePayloads = true)
    {
        QueryOptions queryOptions = new()
        {
            Sort = new SortOption().By<Launch>(launch => launch.DateUtc, sortDirection),
            Populate =
            [
                PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
                    .Selecting<Rocket, string>(rocket => rocket.Name),

                PopulateOption.With<Launch, GuidOrObject<Launchpad>>(launch => launch.Launchpad!)
                    .Selecting<Launchpad, string?>(launchpad => launchpad.FullName)
            ],
            Offset = Math.Max(0, pageIndex - 1) * pageSize,
            Page = pageIndex,
            Limit = pageSize,
            Pagination = true
        };

        if (!includePayloads) return queryOptions;

        PopulateOption populatePayloadsOption = PopulateOption.With<Launch, List<GuidOrObject<Payload>>>(launch => launch.Payloads)
            .Selecting<Payload, string?>(payload => payload.Name)
            .Selecting<Payload, string?>(payload => payload.Type)
            .Selecting<Payload, bool>(payload => payload.Reused)
            .Selecting<Payload, List<string>>(payload => payload.Customers)
            .Selecting<Payload, List<string>>(payload => payload.Manufacturers);

        queryOptions.Populate.Add(populatePayloadsOption);
        return queryOptions;
    }
}