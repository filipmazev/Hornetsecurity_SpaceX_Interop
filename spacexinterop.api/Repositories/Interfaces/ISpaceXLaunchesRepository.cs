using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Repositories.Interfaces;

public interface ISpaceXLaunchesRepository
{
    #region Query Options

    QueryOptions CompleteLaunchesPaginated(int pageSize, int pageIndex, SortDirectionEnum sortDirection = SortDirectionEnum.Descending, bool disableSort = false, bool includePayloads = true);

    #endregion
}