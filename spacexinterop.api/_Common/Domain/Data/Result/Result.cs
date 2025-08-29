using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Utility.Extensions;

namespace spacexinterop.api._Common.Domain.Data.Result;

public class Result
{
    public ResultStatus Status { get; }
    public bool IsSuccess => Status.IsSuccess();
    public bool IsFailure => Status.IsFailure();

    public Error? Error { get; }

    public Result()
    {

    }

    public Result(ResultStatus status = ResultStatus.Default, Error? error = null)
    {
        Status = status;
        Error = error;
    }
}

public class Result<TValue> : Result
{
    internal Result(TValue? value, ResultStatus status, Error? error = null)
        : base(status, error)
    {
        Value = value;
    }

    public TValue? Value { get; }
}