using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Result;

namespace spacexinterop.api._Common.Utility.Factories.Interfaces;

public interface IResultFactory
{
    Result Success();
    Result Failure(Error error);
    Result Failure(List<Error> error);
    Result NotFound();
    Result NotFound(Error error);
    Result<TValue> NotFound<TValue>(TValue value, Error error);
    Result FromStatus(ResultStatus status);
    Result<TValue> FromStatus<TValue>(TValue value, ResultStatus status);
    Result<TValue> Success<TValue>(TValue value);
    Result<TValue> Failure<TValue>(Error error, ResultStatus status = ResultStatus.Failure);
    Result<TValue> Failure<TValue>(List<Error> errors, ResultStatus status = ResultStatus.Failure);
    Result<TValue> Create<TValue>(TValue? value);
    Result Exception(string message, Error? error = null);
    Result Exception(Exception exception, string message, Error? error = null);
    Result<T> Exception<T>(string message, Error? error = null);
    Result<T> Exception<T>(Exception exception, string message, Error? error = null);
}