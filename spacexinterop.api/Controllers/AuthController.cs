using Microsoft.AspNetCore.Mvc;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api.Data.Request;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Services.Interfaces;

namespace spacexinterop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IResultFactory _resultFactory;

    public AuthController(
        IAuthService authService,
        IResultFactory resultFactory)
    {
        _authService = authService;
        _resultFactory = resultFactory;
    }

    [HttpGet("whoami")]
    public async Task<IActionResult> WhoAmI()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            Result validationResult = await _authService.ValidateUserByUserName(User.Identity.Name);

            if(!validationResult.IsSuccess)
                return Unauthorized();

            WhoAmIResponse response = new()
            {
                UserName = User.Identity.Name,
                Email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value
            };

            return Ok(_resultFactory.Success(response));
        }

        return Unauthorized();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Result result = await _authService.Login(request);

        return result.IsFailure
            ? Unauthorized(result.Error)
            : Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Result result = await _authService.Logout();

        return result.IsFailure 
            ? StatusCode(500, result.Error) 
            : Ok(result);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        Result result = await _authService.Signup(request);

        return result.IsFailure 
            ? BadRequest(result.Error) 
            : Ok(result);
    }
}