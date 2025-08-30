using spacexinterop.api.Data.Models.External.Space_X.Launches;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Models.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Repositories;

public partial class SpaceXLaunchesRepository
{
    public QueryOptions CompleteLaunchesPaginated(int pageSize, int pageIndex) => new()
    {
        Sort = new SortOption().By<Launch>(l => l.DateUtc, SortDirection.Ascending),
        Populate =
        [
            PopulateOption.With<Launch, List<GuidOrObject<Capsule>>>(item => item.Capsules)
        ],
        Offset = Math.Max(0, pageIndex - 1) * pageSize,
        Page = pageIndex,
        Limit = pageSize,
        Pagination = true
    };
}