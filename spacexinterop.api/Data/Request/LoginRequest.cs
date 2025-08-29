namespace spacexinterop.api.Data.Request;

public class LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public bool RememberMe { get; init; }
}