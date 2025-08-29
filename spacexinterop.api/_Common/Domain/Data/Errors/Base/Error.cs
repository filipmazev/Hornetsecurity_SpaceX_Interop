using spacexinterop.api._Common.Domain.Data.Result;
using System.Collections.Concurrent;
using System.Net;

namespace spacexinterop.api._Common.Domain.Data.Errors.Base;

public class Error
{
    private static readonly ConcurrentDictionary<string, int> ErrorCodeCounters = new();

    public string Code { get; }
    public string Message { get; }
    public Exception? Exception { get; }
    public HttpStatusCode? HttpStatusCode { get; }

    public static readonly Error None = new("None", "No error.");

    public static Error CreateError(string baseCode, string message)
    {
        string uniqueCode = GenerateUniqueCode(baseCode);
        return new Error(uniqueCode, message);
    }

    private static string GenerateUniqueCode(string baseCode)
    {
        var count = ErrorCodeCounters.AddOrUpdate(baseCode, 1, (key, oldValue) => oldValue + 1);
        return $"{baseCode}_{count:D3}";
    }

    private Error(string code, string message, Exception? exception = null, HttpStatusCode? httpStatusCode = null)
    {
        Code = code;
        Message = message;
        Exception = exception;
        HttpStatusCode = httpStatusCode;
    }

    public ErrorJsonDto ToDto() => new(code: Code, message: Message);
}