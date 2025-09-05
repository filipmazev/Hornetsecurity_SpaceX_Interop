using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;

public interface IBaseJsonQueryModel
{
    [JsonIgnore]
    string JsonQueryFieldName { get; }
}