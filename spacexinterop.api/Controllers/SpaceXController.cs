using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;
using Microsoft.AspNetCore.Mvc;
using spacexinterop.api.Data.Enums.External.Space_X;

namespace spacexinterop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SpaceXController(
    ISpaceXService spaceXService)
    : Controller
{
    [HttpPost(nameof(GetLaunches))]
    public async Task<IActionResult> GetLaunches([FromBody] SpaceXLaunchesRequest request)
    {
        Result<PaginatedResponse<LaunchResponse>?> result = await spaceXService.GetLaunches(request);
        return Ok(result);
    }

    [HttpGet(nameof(GetLatestLaunch))]
    public async Task<IActionResult> GetLatestLaunch()
    {
        Result<LatestLaunchResponse?> result = await spaceXService.GetLatestLaunch();
        return Ok(result);
    }
}