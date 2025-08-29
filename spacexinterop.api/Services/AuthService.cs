using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Errors;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Utility.Validators;
using spacexinterop.api.Services.Interfaces;
using spacexinterop.api.Data.Request;
using Microsoft.AspNetCore.Identity;
using spacexinterop.api.Data.Models;
using spacexinterop.api._Common;

namespace spacexinterop.api.Services;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ILogger<AuthService> logger,
    IValidators validators,
    IResultFactory resultFactory)
    : IAuthService
{
    public async Task<Result> Login(LoginRequest request)
    {
        if (!validators.IsEmailValid(request.Email))
            return resultFactory.Failure(CommonError.Unauthorized);

        try
        {
            User? user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return resultFactory.Failure(CommonError.Unauthorized);

            SignInResult result = await signInManager.PasswordSignInAsync(
                user, request.Password, request.RememberMe, lockoutOnFailure: true);

            return !result.Succeeded 
                ? resultFactory.Failure(CommonError.Unauthorized)
                : resultFactory.Success("Logged in");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Login failed");
            return resultFactory.Exception(ex, "An error occurred during login.");
        }
    }

    public async Task<Result> Logout() 
    {         
        try
        {
            await signInManager.SignOutAsync();
            return resultFactory.Success("Logged out");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Logout failed");
            return resultFactory.Exception(ex, "An error occurred during logout.");
        }
    }

    public async Task<Result> Register(RegisterRequest request)
    {
        if (!validators.IsEmailValid(request.Email))
            return resultFactory.FromStatus(ResultStatus.ValidationFailed);

        User user = new()
        {
            UserName = request.Username,
            Email = request.Email
        };

        try
        {
            IdentityResult result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                bool duplicateEmail = result.Errors.Any(e => e.Code == Constants.IdentityDuplicateEmailCode);
                bool duplicateUserName = result.Errors.Any(e => e.Code == Constants.IdentityDuplicateUserNameCode);

                Error error = duplicateEmail ? AuthError.EmailAlreadyExists 
                    : duplicateUserName ? AuthError.UserNameAlreadyExists 
                    : AuthError.RegistrationFailed;

                ResultStatus errorStatus = duplicateEmail ? ResultStatus.EmailAlreadyExists : ResultStatus.Failure;

                return resultFactory.Failure(error: error, status: errorStatus);
            }
            
            await signInManager.SignInAsync(user, isPersistent: true, authenticationMethod: null);

            return resultFactory.Success("Signup successful");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Signup failed");
            return resultFactory.Exception(ex, "An error occurred during signup.");
        }
    }

    public async Task<Result> ValidateUserByUserName(string? userName)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(userName))
                return resultFactory.FromStatus(ResultStatus.InvalidRequest);

            User? user = await userManager.FindByNameAsync(userName);
            
            return user is null 
                ? resultFactory.FromStatus(ResultStatus.NotFound) 
                : resultFactory.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ValidateUserByUserName failed");
            return resultFactory.Exception(ex, "An error occurred during user validation.");
        }
    }
}