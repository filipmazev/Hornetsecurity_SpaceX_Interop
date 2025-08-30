using System.ComponentModel.DataAnnotations;
using spacexinterop.api._Common;

namespace spacexinterop.api.Data.Request;

public class RegisterRequest
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    [MinLength(Constants.MinPasswordLength)]
    [MaxLength(Constants.MaxPasswordLength)]
    [RegularExpression(Constants.PasswordWithNumberAndSpecialCharRegex, ErrorMessage = "Password must contain at least one number and one special character.")]
    public required string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
}