using Microsoft.AspNetCore.Identity;

namespace spacexinterop.api.Data.Models;

public class User : IdentityUser
{
    [ProtectedPersonalData]
    public override string? Email { get; set; }

    [ProtectedPersonalData]
    public override string? PhoneNumber { get; set; }

    [ProtectedPersonalData]
    public override string? UserName { get; set; }
}