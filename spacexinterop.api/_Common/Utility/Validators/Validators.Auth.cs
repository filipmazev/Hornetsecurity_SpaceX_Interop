using System.Net.Mail;

namespace spacexinterop.api._Common.Utility.Validators;

public partial class Validators
{
    public bool IsEmailValid(string email)
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
}