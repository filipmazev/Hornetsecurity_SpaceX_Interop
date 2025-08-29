using spacexinterop.api._Common.Domain.Data.Errors.Base;

namespace spacexinterop.api._Common.Domain.Data.Errors;

public static class CommonError
{
    public static Error NullValue => Error.CreateError("null_value", "Error, null value");
    public static Error NotFound => Error.CreateError("not_found", "Error, not found");
    public static Error Unauthorized => Error.CreateError("unauthorized", "Error, unauthorized");
    public static Error SomethingWentWrong => Error.CreateError("something_went_wrong", "Error, something went wrong");
}