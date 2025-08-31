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
        status: ResultStatusEnum.Success,
        error: null);

    public Result<TValue> Success<TValue>(TValue value) => new(
        value: value,
        status: ResultStatusEnum.Success,
        error: null);

    public Result Failure(Error error) => new(
        status: ResultStatusEnum.Failure,
        error: error);

    public Result Failure(Error error, ResultStatusEnum status) => new(
        error: error,
        status: status);

    public Result<TValue> Failure<TValue>(Error error) => new(
        value: default,
        status: ResultStatusEnum.Failure,
        error: error);

    public Result<TValue> Failure<TValue>(Error error, ResultStatusEnum status) => new(
        value: default,
        status: status,
        error: error);

    public Result FromStatus(ResultStatusEnum status) => new(
        status: status,
        error: status.ResolveError());

    public Result<TValue> FromStatus<TValue>(ResultStatusEnum status) => new(
        value: default,
        status: status,
        error: status.ResolveError());

    public Result Exception(Exception exception, string message, Error? error = null)
    {
        _logger?.LogError(exception, "Exception: {Message}. Error: {Error}", message, error?.Messages ?? ["None"]);
        return new Result(
            status: ResultStatusEnum.Exception,
            error: error ?? Error.CreateError(
                baseCode: "Exception",
                message: "An exception occurred with message: " + message));
    }

    public Result<TValue> Exception<TValue>(Exception exception, string message, Error? error = null)
    {
        _logger?.LogError(exception, "Exception: {Message}. Error: {Error}", message, error?.Messages ?? ["None"]);
        return new Result<TValue>(
            value: default,
            status: ResultStatusEnum.Exception,
            error: error ?? Error.CreateError(
                baseCode: "Exception",
                message: "An exception occurred with message: " + message));
    }
}