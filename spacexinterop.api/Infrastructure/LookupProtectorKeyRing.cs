using spacexinterop.api._Common._Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace spacexinterop.api.Infrastructure;

public class LookupProtectorKeyRing(IOptions<EncryptionKeyConfig> encryptionKeyConfig) : ILookupProtectorKeyRing
{
    private readonly EncryptionKeyConfig _encryptionKeyConfig = encryptionKeyConfig.Value;

    public string CurrentKeyId => _encryptionKeyConfig.CurrentKeyId;

    public IEnumerable<string> GetAllKeyIds() => _encryptionKeyConfig.Keys.Keys;

    public string this[string keyId] =>
        _encryptionKeyConfig.Keys.TryGetValue(keyId, out var value) 
            ? value 
            : throw new KeyNotFoundException($"Key with id '{keyId}' was not found.");
}