using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using spacexinterop.api._Common;

namespace spacexinterop.api.Data.Models;

public class User : IdentityUser
{
    [ProtectedPersonalData]
    public override string? Email { get; set; }

    [ProtectedPersonalData]
    public override string? PhoneNumber { get; set; }

    [ProtectedPersonalData]
    public override string? UserName { get; set; }

    [Required]
    [MaxLength(Constants.MaxIdentityStringLength)]
    [ProtectedPersonalData]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(Constants.MaxIdentityStringLength)]
    [ProtectedPersonalData]
    public required string LastName { get; set; }
}