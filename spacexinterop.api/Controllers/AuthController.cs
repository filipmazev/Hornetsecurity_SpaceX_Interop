using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Domain.Data.Errors;
using spacexinterop.api.Services.Interfaces;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;
using Microsoft.AspNetCore.Mvc;

namespace spacexinterop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthService authService,
    IResultFactory resultFactory)
    : Controller
{
    [HttpGet(nameof(CheckSession))]
    public async Task<IActionResult> CheckSession()
    {
        if (!(User.Identity?.IsAuthenticated ?? false)) 
            return Ok(resultFactory.Failure(CommonError.Unauthorized, ResultStatusEnum.Unauthorized));

        Result<UserResponse?> result = await authService.ResolveUserByUserName(User.Identity.Name);

        if (!result.IsSuccess || result.Value is null)
            return Ok(resultFactory.Failure(CommonError.Unauthorized, ResultStatusEnum.Unauthorized));

        return Ok(resultFactory.Success(result.Value));
    }

    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Result result = await authService.Login(request);
        return Ok(result);
    }

    [HttpPost(nameof(Logout))]
    public async Task<IActionResult> Logout()
    {
        Result result = await authService.Logout();
        return Ok(result);
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        Result result = await authService.Register(request);
        return Ok(result);
    }
}