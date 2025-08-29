using spacexinterop.api._Common.Utility.Factories.Interfaces;
using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Errors;
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

    public Result Failure(List<Error> errorsList) => new(
        status: ResultStatus.Failure,
        errorsList: errorsList);

    public Result NotFound() => new(
        status: ResultStatus.NotFound,
        error: CommonError.NotFound);

    public Result NotFound(Error error) => new(
        status: ResultStatus.NotFound,
        error: error);

    public Result<TValue> NotFound<TValue>(TValue value, Error error) => new(
        value: value,
        status: ResultStatus.NotFound,
        error: error);

    public Result FromStatus(ResultStatus status) => new(
        status: status,
        error: status.ResolveError());

    public Result<TValue> FromStatus<TValue>(TValue value, ResultStatus status) => new(
        value: value,
        status: status,
        error: status.ResolveError());

    public Result<TValue> Success<TValue>(TValue value) => new(
        value: value,
        status: ResultStatus.Success,
        error: null);

    public Result<TValue> Failure<TValue>(Error error, ResultStatus status = ResultStatus.Failure) => new(
        value: default,
        status: status,
        error: error);

    public Result<TValue> Failure<TValue>(List<Error> errors, ResultStatus status = ResultStatus.Failure) => new(
        value: default,
        status: status,
        errors: errors);

    public Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(CommonError.NullValue);

    public Result Exception(string message, Error? error = null)
    {
        _logger?.LogError("Exception: {Message}. Error: {Error}", message, error?.Message ?? "None");
        return new Result(
            status: ResultStatus.Exception,
            error: error ?? CommonError.SomethingWentWrong);
    }

    public Result Exception(Exception exception, string message, Error? error = null)
    {
        _logger?.LogError(exception, "Exception: {Message}. Error: {Error}", message, error?.Message ?? "None");
        return new Result(
            status: ResultStatus.Exception,
            error: error ?? Error.CreateError(
                baseCode: "Exception",
                message: "An exception occurred with message: " + message));
    }

    public Result<T> Exception<T>(string message, Error? error = null)
    {
        _logger?.LogError("Exception: {Message}. Error: {Error}", message, error?.Message ?? "None");
        return Failure<T>(error ?? CommonError.SomethingWentWrong);
    }

    public Result<T> Exception<T>(Exception exception, string message, Error? error = null)
    {
        _logger?.LogError(exception, "Exception: {Message}. Error: {Error}", message, error?.Message ?? "None");
        return Failure<T>(error ?? CommonError.SomethingWentWrong);
    }
}