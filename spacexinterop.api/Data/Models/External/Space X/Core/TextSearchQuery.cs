using spacexinterop.api.Data.Models.External.Space_X.Core.Interfaces;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Models.External.Space_X.Core;

public class TextSearchQuery(
    string? search,
    string? language = null,
    bool caseSensitive = false,
    bool diacriticSensitive = false)
    : IBaseJsonQueryModel
{
    [JsonIgnore]
    public string JsonQueryFieldName => "$text";

    [JsonPropertyName("$search")]
    public string Search { get; init; } = search ?? string.Empty;

    [JsonPropertyName("$language")]
    public string? Language { get; init; } = language;

    [JsonPropertyName("$caseSensitive")]
    public bool CaseSensitive { get; init; } = caseSensitive;

    [JsonPropertyName("$diacriticSensitive")]
    public bool DiacriticSensitive { get; init; } = diacriticSensitive;
}