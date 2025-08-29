using spacexinterop.api._Common.Utility.Attributes;

namespace spacexinterop.api._Common.Domain.Data.Result;

public enum ResultStatus : byte
{
    #region Basic
    Default,

    [ResultStatusConnotation(IsPositive = true)]
    Success,

    Failure,

    NotFound,
    ValidationFailed,
    Exception,
    InvalidType,
    InvalidRequest,
    #endregion

    #region Auth
    Unauthorized,
    #endregion
}
