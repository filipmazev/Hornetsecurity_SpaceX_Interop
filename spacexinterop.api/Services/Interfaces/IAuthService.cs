using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Data.Response;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services.Interfaces;

public interface IAuthService
{
    Task<Result<UserResponse?>> Login(LoginRequest request);
    Task<Result> Logout();
    Task<Result<UserResponse?>> Register(RegisterRequest request);
    Task<Result<UserResponse?>> ResolveUserByUserName(string? userName);
}