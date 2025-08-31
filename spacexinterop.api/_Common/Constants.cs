namespace spacexinterop.api._Common;

public static class Constants
{
    #region Core

    public const int UserLockoutTimespanInMinutes = 120;
    public const int MaxFailedAccessAttempts = 5;
    public const int SessionDurationInHours = 1;
    public const int MemoryCacheTimeToLiveInMinutes = 10;

    #endregion

    #region Validatory

    public const int MinPasswordLength = 8;
    public const int MaxPasswordLength = 50;

    public const int MaxIdentityStringLength = 200;

    #region Regex

    public const string PasswordWithNumberAndSpecialCharRegex = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*(),.?""{}|<>]).+$";

    #endregion

    #endregion

    #region Error Codes

    public const string IdentityDuplicateEmailCode = "DuplicateEmail";

    #endregion
}