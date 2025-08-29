using System.Collections.Concurrent;
using System.Net;

namespace spacexinterop.api._Common.Domain.Data.Errors.Base;

public class Error
{
    private static readonly ConcurrentDictionary<string, int> ErrorCodeCounters = new();

    public string Code { get; }
    public string[] Messages { get; }
    public Exception? Exception { get; }
    public HttpStatusCode? HttpStatusCode { get; }

    public static Error CreateError(string baseCode, string message)
    {
        string uniqueCode = GenerateUniqueCode(baseCode);
        return new Error(uniqueCode, messages: [message]);
    }

    public static Error CreateError(string baseCode, string[] messages)
    {
        string uniqueCode = GenerateUniqueCode(baseCode);
        return new Error(uniqueCode, messages);
    }

    private static string GenerateUniqueCode(string baseCode)
    {
        int count = ErrorCodeCounters.AddOrUpdate(baseCode, 1, (_, oldValue) => oldValue + 1);
        return $"{baseCode}_{count:D3}";
    }

    private Error(string code, string[] messages, Exception? exception = null, HttpStatusCode? httpStatusCode = null)
    {
        Code = code;
        Messages = messages;
        Exception = exception;
        HttpStatusCode = httpStatusCode;
    }
}