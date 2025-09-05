using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services.Interfaces;

public interface ISpaceXService
{
    Task<Result<PaginatedResponse<LaunchRowResponse>?>> GetLaunchRows(SpaceXLaunchesRequest request);
    Task<Result<CompleteLaunchResponse?>> GetCompleteLaunchById(string id);
}