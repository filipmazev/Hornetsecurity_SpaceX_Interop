using spacexinterop.api._Common.Domain.Data.Errors.Base;

namespace spacexinterop.api._Common.Domain.Data.Errors;

public static class CommonError
{
    public static Error SomethingWentWrong => Error.CreateError("unauthorized", "Error, something went wrong");
    public static Error Unauthorized => Error.CreateError("unauthorized", "Error, unauthorized");
    public static Error EntityNotFound(string entity) => Error.CreateError("not_found", $"Error, {entity} not found");
}