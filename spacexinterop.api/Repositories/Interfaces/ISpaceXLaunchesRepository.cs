using spacexinterop.api.Data.Request.External.Space_X;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Repositories.Interfaces;

public interface ISpaceXLaunchesRepository
{
    SpaceXQueryRequest SimpleLaunchesPaginated(
        string? searchText,
        bool upcoming,
        int pageSize,
        int pageIndex,
        SortDirectionEnum? sortDirection = SortDirectionEnum.Descending);

    SpaceXQueryRequest CompleteLaunchById(string id);
}