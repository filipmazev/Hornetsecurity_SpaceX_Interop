using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [HttpPost(nameof(GetLaunches))]
    public async Task<IActionResult> GetLaunches([FromBody] SpaceXLaunchesRequest request, CancellationToken cancellationToken)
    {
        Result result = await spaceXService.GetLaunches(request, cancellationToken);
        return Ok(result);
    }
}