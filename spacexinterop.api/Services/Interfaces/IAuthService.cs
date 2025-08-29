using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Data.Request;

namespace spacexinterop.api.Services.Interfaces;

public interface IAuthService
{
    Task<Result> Login(LoginRequest request);
    Task<Result> Logout();
    Task<Result> Register(RegisterRequest request);
    Task<Result> ValidateUserByUserName(string? userName);
}