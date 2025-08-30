using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Result;

namespace spacexinterop.api._Common.Utility.Factories.Interfaces;

public interface IResultFactory
{
    Result Success();
    Result<TValue> Success<TValue>(TValue value);
    Result Failure(Error error);
    Result Failure(Error error, ResultStatus status);
    Result<TValue> Failure<TValue>(Error error);
    Result<TValue> Failure<TValue>(Error error, ResultStatus status);
    Result FromStatus(ResultStatus status);
    Result<TValue> FromStatus<TValue>(ResultStatus status);
    Result Exception(Exception exception, string message, Error? error = null);
    Result<TValue> Exception<TValue>(Exception exception, string message, Error? error = null);
}