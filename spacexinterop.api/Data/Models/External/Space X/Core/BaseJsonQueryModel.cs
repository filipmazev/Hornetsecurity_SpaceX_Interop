using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public abstract class BaseJsonQueryModel
{
    [JsonIgnore]
    public abstract string JsonQueryFieldName { get; }

    public abstract Dictionary<string, object?> ToJsonDictionary();
}