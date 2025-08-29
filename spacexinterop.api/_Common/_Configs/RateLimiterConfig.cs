namespace spacexinterop.api._Common._Configs;

public class RateLimiterConfig
{
    public int PermitLimit { get; set; }
    public int WindowSeconds { get; set; }
}