namespace spacexinterop.api._Common;

public static class Constants
{
    #region Core

    public const int UserLockoutTimespanInMinutes = 120;
    public const int MaxFailedAccessAttempts = 5;
    public const int SessionDurationInHours = 1;

    #endregion

    #region Validatory

    public const int MinUsernameLength = 5;
    public const int MaxUsernameLength = 20;

    public const int MinPasswordLength = 8;
    public const int MaxPasswordLength = 50;

    public const int MinNameLength = 4;
    public const int MaxNameLength = 50;

    #region Regex

    public const string PasswordWithNumberAndSpecialCharRegex = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*(),.?""{}|<>]).+$";

    #endregion

    #endregion
}