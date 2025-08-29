using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Result;
using spacexinterop.api._Common.Utility.Extensions;

namespace spacexinterop.api._Common.Utility.Factories;

public class ResultFactory : IResultFactory
{
    private readonly ILogger<ResultFactory>? _logger;

    public ResultFactory()
    {

    }

    public ResultFactory(ILogger<ResultFactory>? logger)
    {
        _logger = logger;
    }

    public Result Success() => new(
        status: ResultStatus.Success,
        error: null);

    public Result Failure(Error error) => new(
        status: ResultStatus.Failure,
        error: error);

    public Result Failure(Error error, ResultStatus status) => new(
        error: error,
        status: status);

    public Result FromStatus(ResultStatus status) => new(
        status: status,
        error: status.ResolveError());

    public Result<TValue> Success<TValue>(TValue value) => new(
        value: value,
        status: ResultStatus.Success,
        error: null);

    public Result Exception(Exception exception, string message, Error? error = null)
    {
        _logger?.LogError(exception, "Exception: {Message}. Error: {Error}", message, error?.Messages ?? ["None"]);
        return new Result(
            status: ResultStatus.Exception,
            error: error ?? Error.CreateError(
                baseCode: "Exception",
                message: "An exception occurred with message: " + message));
    }
}