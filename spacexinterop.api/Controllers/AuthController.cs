using Microsoft.AspNetCore.Mvc;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api.Data.Request;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Services.Interfaces;

namespace spacexinterop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthService authService,
    IResultFactory resultFactory)
    : Controller
{
    [HttpGet("whoami")]
    public async Task<IActionResult> WhoAmI()
    {
        if (!(User.Identity?.IsAuthenticated ?? false)) return Unauthorized();

        Result validationResult = await authService.ValidateUserByUserName(User.Identity.Name);

        if(!validationResult.IsSuccess)
            return Unauthorized();

        WhoAmIResponse response = new()
        {
            UserName = User.Identity.Name,
            Email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value
        };

        return Ok(resultFactory.Success(response));

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Result result = await authService.Login(request);
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Result result = await authService.Logout();
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        Result result = await authService.Register(request);
        return Ok(result);
    }
}