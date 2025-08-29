using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api.Services.Interfaces;
using spacexinterop.api.Data.Request;
using Microsoft.AspNetCore.Identity;
using spacexinterop.api.Data.Models;
using System.Text;

namespace spacexinterop.api.Services;

public partial class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    private readonly ILogger<AuthService> _logger;
    private readonly IResultFactory _resultFactory;

    public AuthService(
        UserManager<User> userManager, 
        SignInManager<User> signInManager,

        ILogger<AuthService> logger,
        IResultFactory resultFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;

        _logger = logger;
        _resultFactory = resultFactory;
    }

    public async Task<Result> Login(LoginRequest request)
    {
        if(!IsEmailValid(request.Email))
            return _resultFactory.FromStatus(ResultStatus.InvalidRequest);

        try
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null) 
                return _resultFactory.FromStatus(ResultStatus.Unauthorized);

            var result = await _signInManager.PasswordSignInAsync(
                user, request.Password, request.RememberMe, lockoutOnFailure: true);

            if (!result.Succeeded)
                return _resultFactory.FromStatus(ResultStatus.Unauthorized);

            return _resultFactory.Success("Logged in");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login faild");
            return _resultFactory.Exception(ex, "An error occurred during login.");
        }
    }

    public async Task<Result> Logout() 
    {         
        try
        {
            await _signInManager.SignOutAsync();
            return _resultFactory.Success("Logged out");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Logout failed");
            return _resultFactory.Exception(ex, "An error occurred during logout.");
        }
    }

    public async Task<Result> Signup(SignupRequest request)
    {
        if (!IsEmailValid(request.Email))
            return _resultFactory.FromStatus(ResultStatus.InvalidRequest);

        User user = new()
        {
            UserName = request.Username,
            Email = request.Email
        };

        try
        {
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                StringBuilder stringBuilder = new(string.Empty);

                foreach (IdentityError error in result.Errors)
                {
                    stringBuilder.AppendLine(error.Description);
                }

                return _resultFactory.Failure(Error.CreateError(baseCode: "SignupFailed", message: stringBuilder.ToString()));
            }
            
            await _signInManager.SignInAsync(user, isPersistent: true, authenticationMethod: null);

            return _resultFactory.Success("Signup successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Signup failed");
            return _resultFactory.Exception(ex, "An error occurred during signup.");
        }
    }

    public async Task<Result> ValidateUserByUserName(string? userName)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(userName))
                return _resultFactory.FromStatus(ResultStatus.InvalidRequest);

            User? user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                return _resultFactory.FromStatus(ResultStatus.NotFound);

            return _resultFactory.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ValidateUserByUserName failed");
            return _resultFactory.Exception(ex, "An error occurred during user validation.");
        }

    }
}