using spacexinterop.api._Common.Domain.Data.Errors.Base;

namespace spacexinterop.api._Common.Domain.Data.Errors;

public static class AuthError
{
    public static Error EmailAlreadyExists => Error.CreateError("duplicate_email", "This email is already registered, do you want to log in instead?");
    public static Error UserNameAlreadyExists => Error.CreateError("duplicate_username", "That username isn’t available. Try another.");
    public static Error RegistrationFailed => Error.CreateError("registration_failed", "Registration failed.");
}