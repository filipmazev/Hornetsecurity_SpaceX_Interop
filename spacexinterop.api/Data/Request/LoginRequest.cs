using System.ComponentModel.DataAnnotations;

namespace spacexinterop.api.Data.Request;

public class LoginRequest
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public bool RememberMe { get; init; }
}