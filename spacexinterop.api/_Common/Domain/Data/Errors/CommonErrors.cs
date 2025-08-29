using spacexinterop.api._Common.Domain.Data.Errors.Base;

namespace spacexinterop.api._Common.Domain.Data.Errors;

public static class CommonError
{
    public static Error Unauthorized => Error.CreateError("unauthorized", "Error, unauthorized");
}