using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Utility.Attributes;
using System.Reflection;

namespace spacexinterop.api._Common.Utility.Extensions;

public static class ResultStatusExtensions
{
    public static bool IsSuccess(this ResultStatus status)
    {
        ResultStatusConnotationAttribute? attribute = status.GetAttribute<ResultStatusConnotationAttribute>();
        return attribute?.IsPositive ?? false;
    }

    public static bool IsFailure(this ResultStatus status) => !status.IsSuccess();

    public static Error? ResolveError(this ResultStatus status)
    {
        return status.IsSuccess()
            ? null
            : Error.CreateError(
                baseCode: "Status",
                message: $"Method returned with status: {status}");
    }

    private static T? GetAttribute<T>(this Enum value) where T : Attribute
    {
        MemberInfo? member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<T>();
    }
}