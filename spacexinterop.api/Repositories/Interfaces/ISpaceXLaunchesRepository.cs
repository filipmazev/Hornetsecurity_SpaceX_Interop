using spacexinterop.api.Data.Models.External.Space_X.Core;

namespace spacexinterop.api.Repositories.Interfaces;

public interface ISpaceXLaunchesRepository
{
    #region Query Options

    QueryOptions CompleteLaunchesPaginated(int pageSize, int pageIndex);

    #endregion
}