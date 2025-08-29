namespace spacexinterop.api.Infrastructure.Interfaces;

public interface ILookupProtector
{
    string? Protect(string keyId, string? data);
    string? Unprotect(string keyId, string? data);
}