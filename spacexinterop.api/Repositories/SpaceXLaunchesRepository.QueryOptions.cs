using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Rockets;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Models.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;
using spacexinterop.api.Data.Models.External;

namespace spacexinterop.api.Repositories;

public partial class SpaceXLaunchesRepository
{
    public QueryOptions CompleteLaunchesPaginated(
        int pageSize, 
        int pageIndex, 
        SortDirectionEnum sortDirection = SortDirectionEnum.Descending,
        bool disableSort = false,
        bool includePayloads = true)
    {
        QueryOptions queryOptions = new()
        {
            Sort = !disableSort ? new SortOption().By<Launch>(launch => launch.DateUtc, sortDirection) : null,
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
    
    public QueryOptions CompleteLaunch()
    {
        return new QueryOptions
        {
            Pagination = false,
            Sort = new SortOption().By<Launch>(launch => launch.DateUtc, SortDirectionEnum.Descending),
            Limit = 1,
            Populate =
            [
                PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
                    .Selecting<Rocket, string>(rocket => rocket.Name),
              
                PopulateOption.With<Launch, List<GuidOrObject<Capsule>>>(launch => launch.Capsules!)
                    .Selecting<Capsule, string>(capsule => capsule.Status)
                    .Selecting<Capsule, string>(capsule => capsule.Type)
                    .Selecting<Capsule, int>(capsule => capsule.ReuseCount)
                    .Selecting<Capsule, int>(capsule => capsule.WaterLandings)
                    .Selecting<Capsule, int>(capsule => capsule.LandLandings)
                    .Selecting<Capsule, string?>(capsule => capsule.LastUpdate),

                PopulateOption.With<Launch, List<Core>>(launch => launch.Cores)
                    .Selecting<Core, int?>(c => c.FlightNumber)
                    .Selecting<Core, bool?>(c => c.Reused)
                    .Selecting<Core, bool?>(c => c.LandingAttempt)
                    .Selecting<Core, bool?>(c => c.LandingSuccess)
                    .PopulateNested<Core, GuidOrObject<Core>>(core => core.CoreItem!, nested =>
                    {
                        nested.Selecting<Core, int?>(c => c.FlightNumber)
                            .Selecting<Core, bool?>(c => c.Reused)
                            .Selecting<Core, bool?>(c => c.LandingAttempt)
                            .Selecting<Core, bool?>(c => c.LandingSuccess);
                    })
                    .PopulateNested<Core, GuidOrObject<Landpad>>(core => core.Landpad!, nested =>
                    {
                        nested.Selecting<Landpad, string?>(c => c.FullName);
                    }),

                PopulateOption.With<Launch, GuidOrObject<Launchpad>>(launch => launch.Launchpad!)
                    .Selecting<Launchpad, string?>(launchpad => launchpad.FullName),

                PopulateOption.With<Launch, List<GuidOrObject<Payload>>>(launch => launch.Payloads)
                    .Selecting<Payload, string?>(payload => payload.Name)
                    .Selecting<Payload, string?>(payload => payload.Type)
                    .Selecting<Payload, bool>(payload => payload.Reused)
                    .Selecting<Payload, List<string>>(payload => payload.Customers)
                    .Selecting<Payload, List<string>>(payload => payload.Manufacturers)
            ]
        };
    }
}