using spacexinterop.api._Common.Domain.Data.Errors.Base;
using spacexinterop.api._Common.Utility.Extensions;

namespace spacexinterop.api._Common.Domain.Data.Result;

public class Result
{
    public ResultStatusEnum Status { get; }
    public bool IsSuccess => Status.IsSuccess();

    public Error? Error { get; }

    public Result()
    {

    }

    public Result(ResultStatusEnum status = ResultStatusEnum.Default, Error? error = null)
    {
        Status = status;
        Error = error;
    }
}

public class Result<TValue> : Result
{
    internal Result(TValue? value, ResultStatusEnum status, Error? error = null)
        : base(status, error)
    {
        Value = value;
    }

    public TValue? Value { get; }
}