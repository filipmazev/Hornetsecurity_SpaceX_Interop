using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Domain.Data.Result;

namespace spacexinterop.api._Common.Utility.Factories.Interfaces;

public interface IResultFactory
{
    Result Success();
    Result Failure(Error error);
    Result Failure(Error error, ResultStatus status);
    Result FromStatus(ResultStatus status);
    Result<TValue> Success<TValue>(TValue value);
    Result Exception(Exception exception, string message, Error? error = null);
}