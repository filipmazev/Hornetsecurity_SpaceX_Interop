using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;
using Microsoft.AspNetCore.Mvc;

namespace spacexinterop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SpaceXController(
    ISpaceXService spaceXService)
    : Controller
{
    [HttpPost(nameof(GetLaunchRows))]
    public async Task<IActionResult> GetLaunchRows([FromBody] SpaceXLaunchesRequest request)
    {
        Result<PaginatedResponse<LaunchRowResponse>?> result = await spaceXService.GetLaunchRows(request);
        return Ok(result);
    }

    [HttpGet(nameof(GetCompleteLaunchById) + "/{id}")]
    public async Task<IActionResult> GetCompleteLaunchById(string id)
    {
        Result<CompleteLaunchResponse?> result = await spaceXService.GetCompleteLaunchById(id);
        return Ok(result);
    }
}