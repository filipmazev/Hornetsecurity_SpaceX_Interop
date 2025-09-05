using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public class GuidOrObject<TModel>
    where TModel : BaseJsonModel, IBaseJsonModel
{
    public string? Guid { get; set; }
    public TModel? Object { get; set; }
}