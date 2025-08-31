using spacexinterop.api.Data.Models.External.Space_X.Core;
using spacexinterop.api.Data.Response.External.Space_X;
using spacexinterop.api.Data.Request.External.Space_X;

namespace spacexinterop.api._Common.Utility.Clients.Interfaces;

public interface ISpaceXClient
{
    Task<SpaceXPaginatedResponse<TModel>?> GetQueryResponse<TModel>(SpaceXQueryRequest queryRequest,
        CancellationToken cancellationToken = default)
        where TModel : BaseJsonModel, new();
}