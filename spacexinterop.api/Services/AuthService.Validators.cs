using System.Net.Mail;

namespace spacexinterop.api.Services;

public partial class AuthService
{
    #region Assertions

    private static bool IsEmailValid(string email)
    {
        try
        {
            MailAddress mail = new(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    #endregion
}
