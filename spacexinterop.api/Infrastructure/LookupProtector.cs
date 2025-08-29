using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace spacexinterop.api.Infrastructure;

public class LookupProtector(ILookupProtectorKeyRing keyRing) : ILookupProtector
{
    private readonly byte[] _initializationVector = [208, 148, 29, 187, 168, 51, 181, 178, 137, 83, 40, 13, 28, 177, 131, 248];

    public string? Protect(string keyId, string? data)
    {
        if (data is null) return null;

        var base64Key = keyRing[keyId];
        byte[] keyBytes = Convert.FromBase64String(base64Key);

        using SymmetricAlgorithm algorithm = Aes.Create();
        
        algorithm.Key = keyBytes;
        algorithm.IV = _initializationVector;

        using ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);
        using MemoryStream ms = new();
        using CryptoStream cryptoStream = new(ms, encryptor, CryptoStreamMode.Write);

        byte[] plainTextBytes = Encoding.UTF8.GetBytes(data);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();

        return Convert.ToBase64String(ms.ToArray());
    }

    public string? Unprotect(string keyId, string? data)
    {
        if (data is null) return null;

        string keyBase64 = keyRing[keyId];
        byte[] keyBytes = Convert.FromBase64String(keyBase64);

        using SymmetricAlgorithm algorithm = Aes.Create();
        
        algorithm.Key = keyBytes;
        algorithm.IV = _initializationVector;

        byte[] cipherTextBytes = Convert.FromBase64String(data);
        using MemoryStream ms = new(cipherTextBytes);
        using ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);
        using CryptoStream cryptoStream = new(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);

        return streamReader.ReadToEnd();
    }
}