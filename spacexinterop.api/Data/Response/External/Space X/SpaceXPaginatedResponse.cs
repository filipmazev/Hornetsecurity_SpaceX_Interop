using spacexinterop.api._Common.Utility.Mapper.Interfaces;
using spacexinterop.api.Data.Models.External.Space_X.Core;
using System.Text.Json.Serialization;

namespace spacexinterop.api.Data.Response.External.Space_X;

public class SpaceXPaginatedResponse<TModel>
    where TModel : BaseJsonModel
{
    [JsonPropertyName("docs")]
    public List<TModel> Docs { get; set; } = [];

    [JsonPropertyName("totalDocs")]
    public int TotalDocs { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pagingCounter")]
    public int PagingCounter { get; set; }

    [JsonPropertyName("hasPrevPage")]
    public bool HasPrevPage { get; set; }

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; set; }

    [JsonPropertyName("prevPage")]
    public int? PrevPage { get; set; }

    [JsonPropertyName("nextPage")]
    public int? NextPage { get; set; }

    public PaginatedResponse<TAppItem> ToPaginatedResponse<TAppItem>(IMapper mapper)
        where TAppItem : class
    {
        List<TAppItem> items = typeof(TAppItem) == typeof(TModel) 
            ? Docs.Cast<TAppItem>().ToList() 
            : Docs.Select(mapper.Map<TModel, TAppItem>).ToList();

        return new PaginatedResponse<TAppItem>
        {
            Items = items,
            TotalItems = TotalDocs,
            PageIndex = Page,
            ItemsPerPage = Limit,
            TotalPages = TotalPages
        };
    }

    public PaginatedResponse<TModel> ToPaginatedResponse()
    {
        return new PaginatedResponse<TModel>
        {
            Items = Docs,
            TotalItems = TotalDocs,
            PageIndex = Page,
            ItemsPerPage = Limit,
            TotalPages = TotalPages
        };
    }
}