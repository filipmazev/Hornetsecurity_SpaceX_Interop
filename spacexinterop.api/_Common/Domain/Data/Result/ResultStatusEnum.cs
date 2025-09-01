using spacexinterop.api._Common.Utility.Attributes;

namespace spacexinterop.api._Common.Domain.Data.Result;

public enum ResultStatusEnum : byte
{
    #region Basic
    Default,

    [ResultStatusConnotation(IsPositive = true)]
    Success,

    Failure,

    NotFound,
    ValidationFailed,
    Exception,
    InvalidRequest,
    #endregion

    #region Auth
    Unauthorized,
    EmailAlreadyExists
    #endregion
}